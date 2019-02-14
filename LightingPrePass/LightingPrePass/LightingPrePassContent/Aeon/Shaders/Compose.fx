float2 TextureSize;

texture ColourMap;
sampler ColourSampler = sampler_state
{
	texture = <ColourMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture LightMap;
sampler LightSampler = sampler_state
{
	texture = <LightMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

struct VSInput
{
	float3 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.TexCoord = input.TexCoord - float2(1.0f / TextureSize.xy);

	return output;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	float3 colour = tex2D(ColourSampler, input.TexCoord).xyz;
	float3 light = tex2D(LightSampler, input.TexCoord).xyz;

	return float4(colour * light.xyz + light.xyz / 2, 1);
}

technique Combine
{
	pass P0
	{
		VertexShader = compile vs_3_0 VSFunct();
		PixelShader = compile ps_3_0 PSFunct();
	}
}
