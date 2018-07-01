var key_up = keyboard_check_pressed(obj_key_mapper.key_up);
var key_down = keyboard_check_pressed(obj_key_mapper.key_down);
var key_left = keyboard_check_pressed(obj_key_mapper.key_left);
var key_right = keyboard_check_pressed(obj_key_mapper.key_right);
var key_enter = keyboard_check_pressed(obj_key_mapper.key_enter);
var key_esc = keyboard_check_pressed(obj_key_mapper.key_esc);
var key_tab = keyboard_check_pressed(obj_key_mapper.key_tab);

var vertical_ans = key_down - key_up;
var horizontal_ans = key_right - key_left;
vertical_cursor += vertical_ans;
horizontal_cursor += horizontal_ans;
switch (tab_cursor) {
	case 0: //Char sel
		vertical_cursor = clamp(vertical_cursor, 0, n_team_size+1);
		switch (vertical_cursor) {
			case 0: //at champ sel
				horizontal_cursor = clamp(horizontal_cursor, 0, n_avail_char-1);
				break;
			case 1: //first champ picked
				break;
		}
		break;
	case 1: //Item sel
		vertical_cursor = (isCursorLeft) ?
						  clamp(vertical_cursor, 0, n_team_size-1) :
						  clamp(vertical_cursor, 0, round(n_items/(n_items_per_row+1)));
		//show_debug_message("item sel, vertical_cursor: " + string(vertical_cursor));
		horizontal_cursor = (isCursorLeft) ?
						  clamp(horizontal_cursor, 0, n_team_size+1) :
						  clamp(horizontal_cursor, 0, n_items_per_row);	
		break;
	case 2: //Confirm
		break;
}


if (key_tab) {
	vertical_cursor = 0;
	horizontal_cursor = 0;
	isCursorLeft = true;
	tab_cursor = (tab_cursor < 2) ? ++tab_cursor : 0;
}

if (key_esc) {
	obj_game_info.goto = rm_fasttravel;
}

if (key_enter) {
	switch (tab_cursor) {
		case 0:
			if (vertical_cursor == 0) {
				image_speed = 1;
				sel_flags[horizontal_cursor] = !sel_flags[horizontal_cursor];
				if (sel_flags[horizontal_cursor]) { //selecting char
					//Look for a free slot and place char there
					for (i = 0; i < n_team_size; ++i) {
						if (tentative_team[i] == "") {
							tentative_team[i] = obj_char_handler.owned_chars[horizontal_cursor];
							break;
						} else if (i = n_team_size-1) {
							//wasn't able to find a free slot
							sel_flags[horizontal_cursor] = false;
						}
					}
				} else { //unselecting char
					//look for which element has the player obj
					for (i = 0; i < n_team_size; ++i) {
						if (tentative_team[i] == obj_char_handler.owned_chars[horizontal_cursor]) {
							tentative_team[i] = "";
						}
					}
				}
			}
			break;
		case 1: //Items page
			if (isCursorLeft) {
				//Selected an item slot to populate with an item
				saved_x = horizontal_cursor;
				saved_y = vertical_cursor;
				horizontal_cursor = 0;
				vertical_cursor = 0;
				isCursorLeft = false;
			} else {
				//Selected an item from right side
				//Convert horizontal_cursor and vertical_cursor to array index
				item_sel[horizontal_cursor+(vertical_cursor*n_items_per_row)] = true;
				show_debug_message("item_sel[" + string(horizontal_cursor+(vertical_cursor*n_items_per_row)) + "] = true");
				//Move cursor back to left pane
				horizontal_cursor = saved_x;
				vertical_cursor = saved_y;
				isCursorLeft = true;
			}
			show_debug_message("DEBUG--------------------------");
			for (i = 0; i < array_length_1d(item_sel); ++i) {
				show_debug_message("item_sel[" + string(i) + "] = " + string(item_sel[i]));
			}
			break;
		case 2: //Confirmation page
			break;
	}

	
}
