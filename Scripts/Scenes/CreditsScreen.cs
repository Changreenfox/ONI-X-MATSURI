using Godot;
using System;

public class CreditsScreen : SceneBase
{
	private int PageNum;
	private TextureRect FirstPage;
	private TextureRect SecondPage;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		isGameplay = false;
		
		PageNum = 1;
		FirstPage = GetNode<TextureRect>("FirstPage");
		SecondPage = GetNode<TextureRect>("SecondPage");
	}

	public void _on_any_input_pressed()
	{
		if (PageNum == 1)
		{
			FirstPage.Hide();
			SecondPage.Show();
			PageNum++;
		}
		else if (PageNum == 2)
		{
			PageNum = 1;
			FirstPage.Show();
			SecondPage.Hide();
			GetTree().Paused = false;
			Hide();
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed == true) {
				_on_any_input_pressed();
			}
		}
		//if (@event is InputEventMouseButton eventMouseButton) {
		//	_on_any_input_pressed();
		//}
	}
	private void _on_ContinueButton_pressed()
	{
		_on_any_input_pressed();
	}
}



