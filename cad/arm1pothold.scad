pot_width = 9.75;
pot_height = 12;
pot_length= 2.2;
pot_holder_width = 2;

skewer_radius = 4.5/2;
skewer_rim = 1.0;

$fn = 50;

module SkewerPipe() {
    skewer_pipe_total = skewer_radius + skewer_rim;
    skewer_height_total = pot_holder_width * 2 + pot_width;
    difference() {
        cylinder(skewer_height_total, skewer_pipe_total, skewer_pipe_total, false);
        cylinder(skewer_height_total, skewer_radius, skewer_radius, false);
    }
}

module PotHolderSide() {
    pot_length_ext = pot_length + 2;
    translate([skewer_radius, -pot_height/2  - pot_holder_width, 0])
    {
        cube([pot_length_ext, pot_holder_width, pot_width], false);
        translate([0, pot_holder_width, 0]) {
            cube([pot_length_ext, pot_height, pot_holder_width], false);
        }
    }
    translate([skewer_radius, pot_height/2, 0])
    {
        cube([pot_length_ext, pot_holder_width, pot_width], false);       
    }
}

union() {
    SkewerPipe();
    PotHolderSide();
}
