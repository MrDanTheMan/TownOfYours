#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

struct VS_IN
{
	float3 Position : POSITION0;
	float4 Color : Color0;
	float2 Texcoord0 : TEXCOORD0;
};

struct PS_IN
{
	float4 Position : POSITION0;
	float4 Color : Color0;
	float2 Texcoord0 : TEXCOORD0;
};

float4x4 STD_WORLD;
float4x4 STD_VIEW;
float4x4 STD_PROJECTION;

texture diffTex;
sampler2D diffTexSampler = sampler_state
{
    Texture = (diffTex);
    MagFilter = Linear;
    MinFilter = Linear;
    AddressU = Clamp;
    AddressV = Clamp;
};

PS_IN VertexMain(VS_IN input)
{
	PS_IN output;
    float4 worldPosition = mul(float4(input.Position, 1.0), STD_WORLD);
    float4 viewPosition = mul(worldPosition, STD_VIEW);
    output.Position = mul(viewPosition, STD_PROJECTION);
    output.Color = input.Color;
    output.Texcoord0 = input.Texcoord0;


	return output;
}

float4 PixelMain(PS_IN input) : COLOR0
{
	float4 diffColor = tex2D(diffTexSampler, input.Texcoord0);
	diffColor.a = 1.0;
	
    return diffColor;
}

technique Main
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexMain();
        PixelShader = compile ps_4_0 PixelMain();
    }
}