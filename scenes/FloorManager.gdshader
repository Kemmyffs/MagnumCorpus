shader_type canvas_item;

uniform sampler2D hole_texture : filter_nearest, hint_default_white;
// 0.0 = Screen is entirely the "hole" (Transparent)
// 1.0 = Hole has shrunk to nothing (Solid Black)
uniform float iris_progress : hint_range(0.0, 8.0) = 0.0;
uniform vec2 hole_center = vec2(0.5, 0.5);

void fragment() {
    // 1. Center the UVs
    vec2 uv = UV - hole_center;
    
    // 2. Fix Aspect Ratio (Prevents the circle from looking like an oval)
    // Adjust this manually or pass it in via C# (e.g., 1920.0/1080.0)
    float aspect = 1.777; 
    uv.x *= aspect;
    
    // 3. The "Snap" Math
    // We use a very high multiplier (30.0) so at 0.0 progress, 
    // the hole is huge. At 1.0, it's tiny.
    float zoom = mix(0.0, 30.0, iris_progress);
    vec2 scaled_uv = uv * zoom + 0.5;
    
    // 4. Sample the hole
    float hole_mask = texture(hole_texture, scaled_uv).a;
    
    // Boundary check: if we are outside the 0.0-1.0 texture range, 
    // it means we are in the "Black" zone.
    if (scaled_uv.x < 0.0 || scaled_uv.x > 1.0 || scaled_uv.y < 0.0 || scaled_uv.y > 1.0) {
        hole_mask = 0.0;
    }

    // COLOR.rgb is the Black of the ColorRect. 
    // We just set the alpha: 0.0 (hole) or 1.0 (black)
    COLOR.a = 1.0 - hole_mask;
}