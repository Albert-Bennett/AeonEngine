struct VSInput
{
	float3 Pos : POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
};

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Normal : COLOR1;
	float4 Depth : COLOR2;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;
	output.Pos = float4(input.Pos, 1);

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	output.Colour = 0.0f;

	output.Normal.rgb = 0.5f;
	output.Normal.a = 0.0f;

	output.Depth = 1.0f;

	return output;
}

technique Clear
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}
