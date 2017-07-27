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
	return float4(1.0, 0.25, 1.0, 1.0);
}

technique Main
{
    pass Pass1
    {
        VertexShader = compile vs_4_0 VertexMain();
        PixelShader = compile ps_4_0 PixelMain();
    }
}