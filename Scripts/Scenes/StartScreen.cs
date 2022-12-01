using Godot;
using System;

public class StartScreen : SceneBase
{

	// the order of the available menu options from left to right
	// note: The names of the options must match the node prefix names in Start.tscn, i.e. "Start" for node "StartButton"
	enum Option {
		Start,
		Credits,
		Quit,
	}

	TextureButton butSel;
	Texture normTexture;

	private const Option LEFT_BOUND_OPTION = Option.Start;
	private const Option RIGHT_BOUND_OPTION = Option.Quit;

	private Option optSel = Option.Start;
	
	private Node2D creditsScreen;

	bool wasPauseLast = false;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		isGameplay = false;
		butSel = GetNode<TextureButton>("CenterContainer/HBoxContainer/"+optSel.ToString()+"Button");
		// save the normal texture for new button
		normTexture = butSel.TextureNormal;
		// highlight the new button
		butSel.TextureNormal = butSel.TextureHover;
		
		creditsScreen = GetNode<Node2D>("CreditsScreen");
	}

	private void update_highlighted_button(Option next) {
		TextureButton butNext = GetNode<TextureButton>("CenterContainer/HBoxContainer/"+next.ToString()+"Button");
		// restore current highlighted to normal
		butSel.TextureNormal = normTexture;
		// save the normal texture for new button
		normTexture = butNext.TextureNormal;
		// highlight the new button
		butNext.TextureNormal = butNext.TextureHover;
		// update the current button
		butSel = butNext;
	}

	public void _on_StartButton_pressed()
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									"res://Scenes/World.tscn"
									);
		//GetTree().ChangeScene("res://Scenes/World.tscn");
	}

	public void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}

	public void _on_CreditsButton_pressed()
	{
		creditsScreen.Show();
		GetTree().Paused = true;
		wasPauseLast = false;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (wasPauseLast == false && @event is InputEventKey eventKey) {
			// GD.Print("here start");
			if (eventKey.Pressed == true) {
				switch (eventKey.Scancode) {
					// move to the left
					case (int)KeyList.A : {
						if (optSel > LEFT_BOUND_OPTION) {
							optSel -= 1;
							update_highlighted_button(optSel);
						}
						break;
					}
					// move to the right
					case (int)KeyList.D : {
						if (optSel < RIGHT_BOUND_OPTION) {
							optSel += 1;
							update_highlighted_button(optSel);
						}
						break;
					}
					// select the highlighted option
					case (int)KeyList.Space : {
						switch (optSel) {
							case Option.Start : {
								_on_StartButton_pressed();
								break;
							}
							case Option.Credits : {
								_on_CreditsButton_pressed();
								break;
							}
							case Option.Quit : {
								_on_QuitButton_pressed();
								break;
							}
						}
						break;
					}
					default : {
						// do nothing
						break;
					}
				}
			}
		}
		// update the variable with last state of pause
		wasPauseLast = GetTree().Paused;
	}

}
