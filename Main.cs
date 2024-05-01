using Godot;
using Godot.Collections;
using System;

public partial class Main : CanvasLayer {
	#region Nodes
	private Button runButton;
	private LineEdit appIDInput;
    private LineEdit remainingTimeInput;
    private LineEdit elapsedTimeInput;
    private Label presetLabel;
    private Sprite2D icon;
	#endregion Nodes

    bool isStarted = false;
	GodotObject discordGDNode;

	public override void _Ready() {
		#region Nodes

		runButton = GetNode<Button>("StartButton");
		appIDInput = GetNode<LineEdit>("AppIDInput");
		remainingTimeInput = GetNode<LineEdit>("RemainingTimeInput");
		elapsedTimeInput = GetNode<LineEdit>("ElapsedTimeInput");
		presetLabel = GetNode<Label>("Label");
		icon = GetNode<Sprite2D>("Icon");

		#endregion Nodes

		discordGDNode = (GodotObject)GD.Load<GDScript>("res://discord_rp.gd").New();

		presetLabel.Text = "";
	}

	public override void _Process(Double delta) {
		GetNode<LinkButton>("AplicationLinkButton").Uri = "https://discord.com/developers/applications/" + appIDInput.Text;
	}

	private void _on_start_button_button_up() {
		if (!isStarted) {
			if (appIDInput.Text != "") {
				presetLabel.Text = "";
				isStarted = true;
				runButton.Text = "Stop!";
				
				Dictionary<string, Variant> data = new() {
					{"APP_ID", appIDInput.Text},
					{"details", GetNode<TextEdit>("DetailsInput").Text},
					{"state", GetNode<LineEdit>("StateInput").Text},
					{"large_image", GetNode<LineEdit>("LargeImageInput").Text},
					{"large_image_text", GetNode<LineEdit>("LargeImageTextInput").Text},
					{"small_image_text", GetNode<LineEdit>("SmallImageTextInput").Text},
					{"small_image", GetNode<LineEdit>("SmallImageInput").Text},
				};

                string startTime = elapsedTimeInput.Text;
				string endTime  = remainingTimeInput.Text;
				
				if (GetNode<CheckBox>("TimeCheckBox").ButtonPressed) {
					startTime = Time.GetUnixTimeFromSystem().ToString();
					endTime = "";
				}

				if (startTime != "") {
					data.Add("start_timestamp", startTime);
				}
				if (endTime != "") {
					data.Add("end_timestamp", endTime);
				}

				discordGDNode.Call("start_rp", data);
			}
			else {
				GetNode<AcceptDialog>("AcceptDialog").Visible = true;
				runButton.Disabled = true;
			}
		}
		else {
			presetLabel.Text = "";
			discordGDNode.Call("stop_rp");
			isStarted = false;
			runButton.Text = "Start!";
		}
	}

	private void _on_button_button_down() {
		int index = (int)(GD.Randi() % 2);
		icon.Frame = index + 1;
	}

	private void _on_button_button_up() {
		icon.Frame = 0;
	}

	private void _on_accept_dialog_confirmed() {
		runButton.Disabled = false;
	}

	private void _on_time_check_box_toggled(bool toggledOn) {
		elapsedTimeInput.Editable = !toggledOn;
		elapsedTimeInput.Text = "";
		remainingTimeInput.Editable = !toggledOn;
		remainingTimeInput.Text = "";
	}

	private void _on_remaining_time_input_text_changed(string newText) {
		if (newText != "") {
			elapsedTimeInput.Editable = false;
			elapsedTimeInput.Text = "";
		}
		else {
			elapsedTimeInput.Editable = true;
		}
	}

	private void _on_elapsed_time_input_text_changed(string newText) {
		if (newText != "") {
			remainingTimeInput.Editable = false;
			remainingTimeInput.Text = "";
		}
		else {
			remainingTimeInput.Editable = true;
		}
	}
}
