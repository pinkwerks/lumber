//UNITY_SHADER_NO_UPGRADE
#ifndef SEAMLESSINCLUDE_INCLUDED
#define SEAMLESSINCLUDE_INCLUDED

#define UNITY_PI            3.14159265359f

void seamless_float(float3 pos, float4 fragCoord, out float2 uv)
{
    // equirectangular UVs

    // normal from interpolated object space position
    float3 normal = normalize(pos);

    // atan returns a value between -pi and pi
    // so we divide by pi * 2 to get -0.5 to 0.5
    float phi = atan2(normal.y, normal.x) / (UNITY_PI * 2.0);

    // 0.0 to 1.0 range
    float phi_frac = frac(phi);

    // acos returns 0.0 at the top, pi at the bottom
    // so we flip the y to align with Unity's OpenGL style
    // texture UVs so 0.0 is at the bottom
    float theta = acos(-normal.y) / UNITY_PI;
    // arbitrary non-zero value that is unique in each quad so the delta is never 0
    float magic = 1.0 + float(fragCoord.x) + 2.0 * float(fragCoord.y);
    float bad = (2.0 * pos.x < pos.y) ? magic : 0.0;
    float badx1 = ddx(bad);
    float bady1 = ddy(bad);
    // Distribute "bad" value horizontally and vertically.
    // In case of coarse derivatives this also eliminates the value
    // calculated by the non-participating pixel, which is important
    bad = (badx1 != 0.0 || bady1 != 0.0) ? magic : 0.0;
    // For fine derivatives we need an extra step to distribute the value
    // to the diagonally opposite side.
    badx1 = ddx(bad);
    if (badx1 != 0.0)
        bad = 1.0;
    uv = float2(bad != 0.0 ? phi_frac : phi, theta);
    //uv = phi_frac;
}


void seamless_half(half3 pos, half4 fragCoord, out half2 uv)
{
    // equirectangular UVs

    // normal from interpolated object space position
    half3 normal = normalize(pos);

    // atan returns a value between -pi and pi
    // so we divide by pi * 2 to get -0.5 to 0.5
    half phi = atan2(normal.z, normal.x) / (UNITY_PI * 2.0);

    // 0.0 to 1.0 range
    half phi_frac = frac(phi);

    // acos returns 0.0 at the top, pi at the bottom
    // so we flip the y to align with Unity's OpenGL style
    // texture UVs so 0.0 is at the bottom
    half theta = acos(-normal.y) / UNITY_PI;
    // arbitrary non-zero value that is unique in each quad so the delta is never 0
    half magic = 1.0 + half(fragCoord.x) + 2.0 * half(fragCoord.y);
    half bad = (2.0 * pos.x < pos.y) ? magic : 0.0;
    half badx1 = ddx(bad);
    half bady1 = ddy(bad);
    // Distribute "bad" value horizontally and vertically.
    // In case of coarse derivatives this also eliminates the value
    // calculated by the non-participating pixel, which is important
    bad = (badx1 != 0.0 || bady1 != 0.0) ? magic : 0.0;
    // For fine derivatives we need an extra step to distribute the value
    // to the diagonally opposite side.
    badx1 = ddx(bad);
    if (badx1 != 0.0)
        bad = 1.0;
    uv = half2(bad != 0.0 ? phi_frac : phi, theta);
}

#endif // SEAMLESSINCLUDE_INCLUDED