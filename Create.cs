image_speed = 0;

c_lteal = $a6bd4e;
c_dteal = $475418;


tab_cursor = 0;
tab_items = ["Team", "Items", "Confirm"];
n_items = array_length_1d(obj_char_handler.owned_items);
for (i = 0; i < n_items; ++i) {
	item_sel[i] = false;
}

vertical_cursor = 0;
horizontal_cursor = 0;
isCursorLeft = true; //For "Items" page
tentative_team = ["", "", "", ""];
tentative_equipped_items = [];
n_team_size = obj_char_handler.team_size;
n_avail_char = array_length_1d(obj_char_handler.owned_chars);
n_items_per_char = 4;
n_items_per_row = 5; //actually +1
for (i = 0; i < n_avail_char; ++i) {
	sel_flags[i] = false;
}

show_debug_message("n_avail_char: " + string(n_avail_char) + " (should be 3)");

