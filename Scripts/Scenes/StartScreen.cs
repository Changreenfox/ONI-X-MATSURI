using Godot;
using System;

public class StartScreen : SceneBase
{
	// the selected button can either be 'start/play' or not 'start/play' ('quit')
	private bool isStartSel = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// update the current selected on-screen button (play or quit)
		bool toggleR = Input.IsActionPressed("ArrowRight");
		bool toggleL = Input.IsActionPressed("ArrowLeft");
		// move to the 'quit' button
		if (toggleR == true && isStartSel == true) {
			isStartSel = false;
		// move to the 'play' button
		} else if (toggleL == true && isStartSel == false) {
			isStartSel = true;
		}

		// check if the user as confirmed their choice
		bool isReqAct = Input.IsActionPressed("ActionA");
		if (isReqAct == true) {
			// play :)
			if (isStartSel == true) {
				GetTree().ChangeScene("res://Scenes/World.tscn");
			// quit :(
			} else {
				GetTree().Quit();
			}
		}
	}

	public void _on_StartButton_pressed()
	{
		GetTree().ChangeScene("res://Scenes/World.tscn");
	}

	public void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}

}
