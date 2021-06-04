//A pseudorandom function
uint Hash(uint state){
    state ^= 2747636419u;
    state *= 2654435769u;
    state ^= state >> 16;
    state *= 2654435769u;
    state ^= state >> 16;
    state *= 2654435769u;
    return state;
}

//Remap a generic uint to [0,1] range
float ScaleToRange01(uint number){
    return number/4294967295.0;
}

//Calculates the mean color value of a point and its neghbours
float4 CalculateBlur(uint2 coords, RWTexture2D<float4> map, int width, int height){
    float4 originalValue = map[coords.xy];

    float4 sum = 0;
    for (int offsetX = -1; offsetX <= 1; offsetX++){        
        for (int offsetY = -1; offsetY <= 1; offsetY++){
            int sampleX = coords.x + offsetX;
            int sampleY = coords.y + offsetY;
            if (sampleX >= 0 && sampleX < width && sampleY >= 0 && sampleY <= height){
                sum += map[int2(sampleX, sampleY)];
            }
        }
    }
    return sum / 9;
}
