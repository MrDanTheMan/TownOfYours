using System;
using Microsoft.Xna.Framework.Input;

namespace TownOfYours.Core
{
    /// <summary>
    /// Input action types
    /// </summary>
    public enum INPUT_ACTION
    {
        UP,
        DOWN,    
        MOVED,
    }

    /// <summary>
    /// Defines type fo mouse wheel actions
    /// </summary>
    public enum MOUSE_WHEEL_ACTION
    {
        NONE,
        UP,
        DOWN,
    }

    /// <summary>
    /// Mouse button types
    /// </summary>
    public enum MOUSE_BUTTON
    {
        LEFT,
        RIGHT,
        MIDDLE,
    }

    /// <summary>
    /// Keyboard events arguments data set
    /// </summary>
    public struct KeyboardEventArgs
    {
        public Keys m_key;
        public INPUT_ACTION m_state;
    }

    /// <summary>
    /// Mouse events arguments data set
    /// </summary>
    public struct MouseEventArgs
    {
        public float m_x;
        public float m_y;
        public MOUSE_BUTTON m_button;
        public INPUT_ACTION m_state;
    }

    // Event sigantures
    public delegate void KeyboardEvenHandler(object sender, KeyboardEventArgs args);
    public delegate void MouseEvenHandler(object sender, MouseEventArgs args);
    public delegate void MouseWheelEventHandler(object sender, MOUSE_WHEEL_ACTION action);


    /// <summary>
    /// Class for handling user input
    /// </summary>
    public class InputManager
    {
        public event KeyboardEvenHandler KeyDown;
        public event KeyboardEvenHandler KeyChanged;
        public event MouseEvenHandler MouseButtonDown;
        public event MouseEvenHandler MouseButtonUp;
        public event MouseEvenHandler MouseMoved;
        public event MouseWheelEventHandler MouseWheelMoved;

        private int m_mouseWheel=0;
        private int m_previousMouseWheel=0;

        private readonly int MOUSE_BUTTON_COUNT = 3;
        private readonly float MOUSE_PRECISION = 0.001f;

        private readonly int m_keysCount;
        private readonly bool[] m_currentKeys;
        private readonly bool[] m_previousKeys;
        private readonly bool[] m_currentButtons;
        private readonly bool[] m_previousButtons;

        /// <summary>
        /// Gets current mouse 'X' coordinate
        /// </summary>
        public float MouseX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets current mouse 'Y' coordinate
        /// </summary>
        public float MouseY
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets previous mouse 'X' coordinate
        /// </summary>
        public float PreviousMouseX
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets previous mouse 'Y' coordinate
        /// </summary>
        public float PreviousMouseY
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public InputManager()
        {
            m_keysCount = Enum.GetValues(typeof(Keys)).Length;
            m_currentKeys = new bool[m_keysCount];
            m_previousKeys = new bool[m_keysCount];
            m_currentButtons = new bool[MOUSE_BUTTON_COUNT];
            m_previousButtons = new bool[MOUSE_BUTTON_COUNT];
        }

        /// <summary>
        /// Manages key states
        /// </summary>
        public void Update()
        {
            UpdateMouseInput();
            UpdateKeyboardInput();
        }

        /// <summary>
        /// Pools mouse input values and dispatches events
        /// </summary>
        private void UpdateMouseInput()
        {
            MouseState state = Mouse.GetState();

            m_previousMouseWheel = m_mouseWheel;
            m_mouseWheel = state.ScrollWheelValue;
            PreviousMouseX = MouseX;
            PreviousMouseY = MouseY;
            MouseX = state.X;
            MouseY = state.Y;

            if (MouseX - PreviousMouseX > MOUSE_PRECISION || MouseY - PreviousMouseY > MOUSE_PRECISION)
            {
                NotifyMouseInput(0, INPUT_ACTION.MOVED);
            }

            Array.Copy(m_currentButtons, m_previousButtons, MOUSE_BUTTON_COUNT);
            m_currentButtons[0] = state.LeftButton == ButtonState.Pressed;
            m_currentButtons[1] = state.RightButton == ButtonState.Pressed;
            m_currentButtons[2] = state.MiddleButton == ButtonState.Pressed;

            for(int i=0; i < MOUSE_BUTTON_COUNT; i++)
            {
                if (m_currentButtons[i])
                {
                    NotifyMouseInput((MOUSE_BUTTON)i, INPUT_ACTION.DOWN);
                }

                if (m_currentButtons[i] == m_previousButtons[i])
                {
                    continue;
                }

                if (!m_currentButtons[i])
                {
                    NotifyMouseInput((MOUSE_BUTTON)i, INPUT_ACTION.UP);
                    // TODO: Mouse up event
                }
            }

            if(m_mouseWheel > m_previousMouseWheel)
            {
                MouseWheelMoved?.Invoke(this, MOUSE_WHEEL_ACTION.UP);
            }
            else if(m_mouseWheel < m_previousMouseWheel)
            {
                MouseWheelMoved?.Invoke(this, MOUSE_WHEEL_ACTION.DOWN);
            }
        }

        /// <summary>
        /// Pools keyboard input values and dispatches events
        /// </summary>
        private void UpdateKeyboardInput()
        {
            Array.Copy(m_currentKeys, m_previousKeys, m_keysCount);
            KeyboardState states = Keyboard.GetState();

            // Fetch current key states
            for (int i = 0; i < m_keysCount; i++)
            {
                Keys thisKey = (Keys)i;
                bool currentState = states.IsKeyDown(thisKey);
                m_currentKeys[i] = currentState;

                if (currentState)
                {
                    NotifyKeyDown(thisKey);
                }
            }

            // Compare and notify of state changes
            for (int i = 0; i < m_keysCount; i++)
            {
                if (m_currentKeys[i] == m_previousKeys[i])
                {
                    continue;
                }

                if (m_currentKeys[i])
                {
                    NotifyKeyChanged((Keys)i, INPUT_ACTION.DOWN);
                }
                else
                {
                    NotifyKeyChanged((Keys)i, INPUT_ACTION.UP);
                }
            }
        }

        /// <summary>
        /// Dispatches 'KeyDown' event
        /// </summary>
        /// <param name="key">Key ID</param>
        private void NotifyKeyDown(Keys key)
        {
            KeyboardEventArgs args = new KeyboardEventArgs
            {
                m_key = key,
                m_state = INPUT_ACTION.DOWN
            };

            //Debug.Print("[InputManager] Key Down: {0} !", key.ToString());
            KeyDown?.Invoke(this, args);
        }

        /// <summary>
        /// Dispatches 'KeyChanged' event
        /// </summary>
        /// <param name="key">Key ID</param>
        /// <param name="state">Input description</param>
        private void NotifyKeyChanged(Keys key, INPUT_ACTION state)
        {
            KeyboardEventArgs args = new KeyboardEventArgs
            {
                m_key = key,
                m_state = state
            };

            //Debug.Print("[InputManager] Key state changed: {0} -> {1} !", key.ToString(), state.ToString());
            KeyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Resolves and dispatches approprieate mouse event
        /// </summary>
        /// <param name="button">Mouse button</param>
        /// <param name="action">Input action type</param>
        private void NotifyMouseInput(MOUSE_BUTTON button, INPUT_ACTION action)
        {
            MouseEventArgs args = new MouseEventArgs
            {
                m_button = button,
                m_state = action,
                m_x = MouseX,
                m_y = MouseY
            };

            switch (action)
            {
                case INPUT_ACTION.DOWN:
                    //Debug.Print("[InputManager] Mouse down: button:{0} x:{1} y:{2}", button.ToString(), MouseX.ToString(), MouseY.ToString());
                    MouseButtonDown?.Invoke(this, args);
                    break;

                case INPUT_ACTION.UP:
                    //Debug.Print("[InputManager] Mouse up: button:{0} x:{1} y:{2}", button.ToString(), MouseX.ToString(), MouseY.ToString());
                    MouseButtonUp?.Invoke(this, args);
                    break;

                case INPUT_ACTION.MOVED:
                    //Debug.Print("[InputManager] Mouse moved: x:{0} y:{1}", MouseX.ToString(), MouseY.ToString());
                    MouseMoved?.Invoke(this, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }
    }
}
