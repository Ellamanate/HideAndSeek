void GaussianBlur_float(UnityTexture2D Texture, float2 UV, float Blur, float TexelSizeX, float TexelSizeY, UnitySamplerState Sampler, out float3 Out_RGB, out float Out_Alpha)
{
    float4 col = float4(0.0, 0.0, 0.0, 0.0);
    float kernelSum = 0.0;

    int upper = ((Blur - 1) / 2);
    int lower = -upper;

    for (int x = lower; x <= upper; ++x)
    {
        for (int y = lower; y <= upper; ++y)
        {
            kernelSum++;

            float2 offset = float2(TexelSizeX * x, TexelSizeY * y);
            col += Texture.Sample(Sampler, UV + offset);
        }
    }

    col /= kernelSum;
    Out_RGB = float3(col.r, col.g, col.b);
    Out_Alpha = col.a;
}