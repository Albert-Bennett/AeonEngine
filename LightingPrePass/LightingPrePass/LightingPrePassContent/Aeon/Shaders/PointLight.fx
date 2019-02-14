float4x4 World;
float4x4 View;
float4x4 Proj;

float4x4 IView;
float4x4 IViewProj;

float3 CamPos;
float2 GDSize;

float3 Pos;
float4 Colour;
float Intensity;
float Radius;

texture Opaque;
sampler OpaqueSampler = sampler_state
{
	Texture = <Opaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

struct VSInput
{
	float4 Pos: POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float4 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	float4 viewPos = mul(worldPos, View);

	output.Pos = mul(viewPos, Proj);
	output.ScreenPos = output.Pos;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
	input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

	half4 norm = tex2D(OpaqueSampler, uv);
	half3 normal = mul(2.0f * (norm.xyz) - 1.0f, IView);

	float specInten = tex2D(DepthSampler, uv).a;
	float specPow = norm.a;

	float depth = tex2D(DepthSampler, uv).r;

	float4 pos = 1.0f;
	pos.xy = input.ScreenPos.xy;
	pos.z = depth;

	pos = mul(pos, IViewProj);
	pos /= pos.w;

	float3 lightVec = Pos - pos;
	float attenuation = saturate(1.0f - length(lightVec)/ Radius);
	lightVec = normalize(lightVec);

	float nl = max(0, dot(normal, lightVec));
	float3 diffuse = nl * Colour.xyz;

	float3 r = normalize(reflect(-lightVec, normal));
	float eye = normalize(CamPos - pos.xyz);
	float spec = specInten * pow(saturate(dot(r, eye)), specPow);

	return attenuation * Intensity * float4(diffuse.rgb, spec);
}

technique Lighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
