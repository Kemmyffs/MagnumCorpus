shader_type canvas_item;

uniform float fV : hint_range(0.0, 1.0);
uniform vec3 baseColor : source_color;
uniform float speed : hint_range(0.0, 3.0) = 0.3;

void fragment() {
    // Coordinate setup
    vec2 uv = (UV - 0.5) * 2.0;
    uv.y *= -1.0;

    float sdf = length(uv);

    // HARD CIRCLE MASK
    float mask = step(sdf, 0.88);

    // Wave Math
    float vB = smoothstep(0.1, 0.9, sin(uv.x + (PI * 0.5)) - 0.3);
    float vBA = vB * sin(TIME * speed * 4.0) * 0.1;
    
    float fW = (sin(((TIME * speed * 2.0) + uv.x) * 2.0) * 0.05) + vBA;
    float bW = (sin(((TIME * speed * -2.0) + uv.x) * 2.0 + PI) * 0.05) - vBA;
    
    float fA = (sin(TIME * speed * 4.0) * 0.05) * vB;
    
    float fP = mix(-1.1, 1.1, fV) + (sin(TIME * speed * PI) * 0.1);
    
    // Front and Back wave masks
    float fF = step(uv.y, (fA + fW) + fP);
    float bF = step(uv.y, (-fA + bW) + fP);

    // --- COLOR LOGIC ---
    
    // Define a darker version of the base color for the background wave
    vec3 shadeColor = baseColor * 0.6; 
    
    // Mix the colors: 
    // If front wave (fF) is 1.0, we see baseColor.
    // If only back wave (bF) is 1.0, we see shadeColor.
    vec3 finalColor = mix(shadeColor, baseColor, fF);

    // Calculate total liquid area and final alpha
    float liquidArea = clamp(fF + bF, 0.0, 1.0);
    float alpha = liquidArea * mask;

    COLOR = vec4(finalColor, alpha);
}