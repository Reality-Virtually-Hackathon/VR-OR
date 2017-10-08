pot_width = 12;
pot_length = 10.5;
pot_height = 3;
pot_margin = 6;

legs_height = 3;

difference() {
    cube([pot_width + pot_margin, pot_length + pot_margin, pot_height]);
    translate([pot_margin/2, pot_margin/2, 0]) {
        cube([pot_width, pot_length, pot_height]);
    }
}

difference() {
    union() {
        translate([0, 0, pot_height]) {
            cube([pot_margin, pot_margin, legs_height]);
        }

        translate([0, pot_length, pot_height]) {
            cube([pot_margin, pot_margin, legs_height]);
        }

        translate([pot_width, pot_length, pot_height]) {
            cube([pot_margin, pot_margin, legs_height]);
        }

        translate([pot_width, 0, pot_height]) {
            cube([pot_margin, pot_margin, legs_height]);
        }
    }
    
    translate([pot_margin/2, pot_margin/2, 0]) {
        cube([pot_width, pot_length, pot_height*3]);
    }
}
    