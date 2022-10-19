shader_type canvas_item;
uniform float opacity: hint_range(0.0, 1.0) = 1;
uniform bool flashing = false;
uniform float red = 0.0f;
uniform float green = 0.0f;
uniform float blue = 0.0f;

void fragment(){
	vec4 prev = texture(TEXTURE, UV);
	if(flashing){
		vec4 flash_color = vec4(red, green, blue, 1);
		prev.rgb = mix(prev.rgb, flash_color.rgb, opacity);
	}
	COLOR = prev;
} 