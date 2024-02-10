sampler2D _MainTex;
float4 _MainTex_ST;

sampler2D _Hatch0;
sampler2D _Hatch1;
float4 _LightColor0;

v2f vert (appdata v)
{
    v2f o;
    o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
    o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
    o.nrm = mul(float4(v.norm, 0.0), unity_WorldToObject).xyz;
    return o;
}

fixed4 frag (v2f i) : SV_Target
{
    fixed4 color = tex2D(_MainTex, i.uv);
    half3 diffuse = color.rgb * _LightColor0.rgb * dot(_WorldSpaceLightPos0, normalize(i.nrm));

    //hatching logic goes here

    return color;
}