draw_set_halign(fa_left);
draw_set_valign(fa_top);


//Tabs that show Character/Item
draw_set_font(font_Description);
for (i = 0; i < 3; ++i) {
	if (tab_cursor == i) {
		draw_rectangle_color(38+(i*200), 48, 238+(i*200), 70,
					c_dteal, c_dteal, c_dteal, c_dteal, false);
		draw_set_color(c_white);
		draw_text(40+(i*200), 50, tab_items[i])
	} else {
		draw_rectangle_color(38+(i*200), 48, 238+(i*200), 70,
					c_lteal, c_lteal, c_lteal, c_lteal, false);
		draw_set_color(c_black);
		draw_text(40+(i*200), 50, tab_items[i])
	}	
}

switch (tab_cursor) {
	case 0:
		draw_self();
		//Rectangle that contains character portraits
		draw_rectangle_color(40, 52, 1400, 250,
							 c_lteal, c_lteal, c_lteal, c_lteal, true);
		for(i = 0; i < n_avail_char; ++i) {
			var sel_flag = false;
			for (j = 0; j < n_team_size; ++j) {
				sel_flag = (tentative_team[j] == obj_char_handler.owned_chars[i]) ? true : sel_flag;
			}
			if (sel_flag) {
				shader_set(shdr_sel_portrait_grey);
			}
			switch (obj_char_handler.owned_chars[i]) {
				case obj_jazz:
					draw_sprite(spr_jazz_portrait, 0, 70+(i*220), 66);
					break;
				case obj_carly:
					draw_sprite(spr_carly_portrait, 0, 70+(i*220), 66);
					break;
				case obj_bob:
					draw_sprite(spr_bob_portrait, 0, 70+(i*220), 66);
					break;
			}
			if (sel_flag) {
				shader_reset();
			}
		}

		//Rectangle that highlights character portraits
		if (vertical_cursor == 0) {
			draw_rectangle_color(70+(horizontal_cursor*220), 60, 200+(horizontal_cursor*220), 240,
								 c_red, c_red, c_red, c_red, true);
		}
		//Rectangle that contains picked char and abilities
		draw_rectangle_color(20, 280, 1900, 1000,
							 c_black, c_black, c_black, c_black, true);

					 
		//Rectangle for chars and their abilities
		for (i = 0; i < n_team_size; ++i) {
			draw_rectangle_color(40, 300+(i*160), 1860, 440+(i*160),
								 c_lteal, c_lteal, c_lteal, c_lteal, true);
		}

		//Text
		draw_set_font(font_Description);
		draw_set_color(c_black);
		var disp_ctr = 0;
		for (i = 0; i < n_team_size; ++i) {
			var name = "";
			switch (tentative_team[i]) { 
				case obj_jazz:
					name = "Jazz";
					break;
				case obj_carly:
					name = "Carly";
					break;
				case obj_bob:
					name = "Bob";
					break;
				default:
					name = "";
					break;
			}
			if (tentative_team[i] != "") {
				draw_text(60, 310+(disp_ctr*180), name);
				++disp_ctr;
			}
		}
		break;
	case 1: //Items
		
		//Rectangles that hold char portraits and items
		var disp_ctr = 0;
		for (i = 0; i < n_team_size; ++i) {
			draw_rectangle_color(40, 100+(i*160), 760, 240+(i*160),
								 c_lteal, c_lteal, c_lteal, c_lteal, true);
			for (j = 0; j < n_team_size; ++j) {
				if (tentative_team[i] != "") {
					switch (tentative_team[i]) {
						case obj_jazz:
							draw_sprite(spr_jazz_portrait, 0, 42,  104+(disp_ctr*160));
							break;
						case obj_carly:
							draw_sprite(spr_carly_portrait, 0, 42,  104+(disp_ctr*160));
							break;
						case obj_bob:
							draw_sprite(spr_bob_portrait, 0, 42,  104+(disp_ctr*160));
							break;
					}
					++disp_ctr;
					break;
				}
			}
		}
		
		//Rectangles that hold item grid and descriptions
		draw_rectangle_color(800, 100, 1860, 1000,
								c_lteal, c_lteal, c_lteal, c_lteal, true);
		var row_ctr = 0;
		var row_ptr = 0;
		for (i = 0; i < n_items; ++i) {
			result_array = scr_get_item_spr_name(obj_char_handler.owned_items[i]);
			draw_sprite(result_array[0], 0, 860+(row_ctr*90), 140+(row_ptr*90));
			if (item_sel[i]) {
				draw_set_alpha(0.5);
				draw_rectangle_color(824+(row_ctr*90), 104+(row_ptr*86), 896+(row_ctr*90), 176+(row_ptr*86),
									 c_black, c_black, c_black, c_black, false);
				draw_set_alpha(1.0);
			}
			if (row_ctr >= n_items_per_row) {
				row_ctr = 0;
				row_ptr++;
			} else {
				++row_ctr;
			}
			//show_debug_message("-------------");
			//show_debug_message("row_ctr, row_ptr, spr: " + string(row_ctr) + ", " + string(row_ptr) + ", " +
			//				   sprite_get_name(result_array[0]));
		}
		
		//Rectangles that show available slots per char
		for (i = 0; i < n_team_size; ++i) {
			for (j = 0; j < n_items_per_char; ++j) {
				if (isCursorLeft && (vertical_cursor == i) && (horizontal_cursor == j)) {
					draw_rectangle_color(200+(j*120), 120+(i*180), 280+(j*120), 220+(i*180),
								 c_red, c_red, c_red, c_red, true);
				} else {
					draw_rectangle_color(200+(j*120), 120+(i*180), 280+(j*120), 220+(i*180),
								 c_black, c_black, c_black, c_black, true);
				}
			}
		}
		
		//Rectangle that shows current cursor position on right hand pane
		if (!isCursorLeft) {
			draw_rectangle_color(820+(horizontal_cursor*90), 100+(vertical_cursor*86), 900+(horizontal_cursor*90), 180+(vertical_cursor*86),
								 c_red, c_red, c_red, c_red, true);
		}
		
		
		break;
	case 2: //Confirm
		break;
}


