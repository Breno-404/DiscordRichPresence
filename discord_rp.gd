extends Node

func start_rp(data : Dictionary):
    print(data)
    DiscordRPC.app_id = int(data["APP_ID"])
    DiscordRPC.details = data["details"]
    DiscordRPC.state = data["state"]
    DiscordRPC.large_image = data["large_image"]
    DiscordRPC.large_image_text = data["large_image_text"]
    DiscordRPC.small_image = data["small_image"]
    DiscordRPC.small_image_text = data["small_image_text"]
    if data.has("start_timestamp"): DiscordRPC.start_timestamp = int(data["start_timestamp"])
    if data.has("end_timestamp"): DiscordRPC.end_timestamp = int(Time.get_unix_time_from_system()) + int(data["end_timestamp"])
    DiscordRPC.refresh()

func stop_rp():
    DiscordRPC.clear()