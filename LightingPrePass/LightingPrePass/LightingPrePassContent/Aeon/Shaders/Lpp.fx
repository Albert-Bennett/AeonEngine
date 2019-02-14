float4x4 World;
float4x4 View;
float4x4 Proj;
float4x4 WorldView;

texture Texture;
sampler TextureSampler = sampler_state
{
	texture = <Texture>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture NormalMap;
sampler NormalSampler = sampler_state
{
	texture = <NormalMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture SpecularMap;
sampler SpecularSampler = sampler_state
{
	texture = <SpecularMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

struct VSInput
{
	float4 Pos : POSITION0;
	float3 Norm : NORMAL0;
	float2 TexCoord : TEXCOORD0;
	float3 Tan : TANGENT0;
	float3 BiTan : BINORMAL0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Depth : TEXCOORD1;
	float3x3 BiTanNorm : TEXCOORD2;
};

struct PSOutput
{
	float4 Norm : COLOR0;
	float4 Depth : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.Depth.x = output.Pos.z;
	output.Depth.y = output.Pos.w;
	output.Depth.z = viewPos.z;

	output.BiTanNorm[0] = normalize(mul(input.Tan, (float3x3)WorldView));
	output.BiTanNorm[1] = normalize(mul(input.BiTan, (float3x3)WorldView));
	output.BiTanNorm[2] = normalize(mul(input.Norm, (float3x3)WorldView));

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	half3 normal = tex2D(NormalSampler, input.TexCoord).xyz * 2.0f - 1.0f;
	normal = normalize(mul(normal, input.BiTanNorm));

	normal = normalize(normal);
	normal = 0.5f * (normal + 1.0f);

	output.Norm.xyz = normal;
	output.Norm.a = tex2D(SpecularSampler, input.TexCoord).g;

	output.Depth = input.Depth.x / input.Depth.y;
	output.Depth.y = input.Depth.z;
	output.Depth.a = tex2D(SpecularSampler, input.TexCoord).b;

	return output;
}

technique Opaque
{
	pass P0
	{
		VertexShader = compile vs_3_0 VSFunct();
		PixelShader = compile ps_3_0 PSFunct();
	}
}

struct RVSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord: TEXCOORD0;
};

RVSOutput RVSFunct(float2 texCoord : TEXCOORD0, float4 pos: POSITION0)
{
	RVSOutput output;

	float4 worldPos = mul(pos, World);
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.TexCoord = texCoord;

	return output;
}

float4 RPSFunct(RVSOutput input):COLOR0
{
	float4 colour = tex2D(TextureSampler, input.TexCoord);

	return colour;
}

technique Render
{
	pass P0
	{
		VertexShader = compile vs_3_0 RVSFunct();
		PixelShader = compile ps_3_0 RPSFunct();
	}
}


