using Godot;
using System;

public class Pause : Control
{
	private GameManager gManager;

	enum Option {
		Resume,
		Quit,
	}

	TextureButton butSel;
	Texture normTexture;

	private const Option TOP_BOUND_OPTION = Option.Resume;
	private const Option BOTTOM_BOUND_OPTION = Option.Quit;

	private Option optSel = Option.Resume;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");

		butSel = GetNode<TextureButton>("CenterContainer/VBoxContainer/CenterContainer/VBoxContainer/"+optSel.ToString());
		// save the normal texture for new button
		normTexture = butSel.TextureNormal;
		// highlight the new button
		butSel.TextureNormal = butSel.TextureHover;
	}

	private void update_highlighted_button(Option next) {
		TextureButton butNext = GetNode<TextureButton>("CenterContainer/VBoxContainer/CenterContainer/VBoxContainer/"+next.ToString());
		// restore current highlighted to normal
		butSel.TextureNormal = normTexture;
		// save the normal texture for new button
		normTexture = butNext.TextureNormal;
		// highlight the new button
		butNext.TextureNormal = butNext.TextureHover;
		// update the current button
		butSel = butNext;
	}

	private void _on_Pause_pressed()
	{
		GetTree().Paused = true;
		Show();
	}
	
	private void _on_Resume_pressed()
	{
		GetTree().Paused = false;
		Hide();
	}
	
	private void _on_Quit_pressed()
	{
		Hide();
		GetTree().Paused = false;
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									"res://Scenes/Start.tscn"
									);
		//GetTree().ChangeScene("res://Scenes/Start.tscn");
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed == true) {
				switch (eventKey.Scancode) {
					// move down to next option
					case (int)KeyList.S : {
						if (optSel < BOTTOM_BOUND_OPTION) {
							optSel += 1;
							update_highlighted_button(optSel);
						}
						break;
					}
					// move up to previous option
					case (int)KeyList.W : {
						if (optSel > TOP_BOUND_OPTION) {
							optSel -= 1;
							update_highlighted_button(optSel);
						}
						break;
					}
					// confirm choice
					case (int)KeyList.Space : {
						switch (optSel) {
							case Option.Resume : {
								_on_Resume_pressed();
								break;
							}
							case Option.Quit : {
								_on_Quit_pressed();
								break;
							}
						}
						break;
					}
					default : {
						break;
					}
				}
			}
		}
	}
}





