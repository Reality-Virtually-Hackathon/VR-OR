pot_width = 9.75;
pot_height = 12;
pot_length= 4;
pot_holder_width = 3;

wheel_radius = 25;
wheel_radius_raise = 3;

skewer_radius = 4.5/2;
skewer_rim = 1.0;
pot_ext = 2;

$fn = 50;

module PotHolderSide() {
    pot_length_ext = pot_length + pot_ext;
    translate([0, -pot_height/2  - pot_holder_width, 0])
    {
        cube([pot_length_ext, pot_holder_width, pot_width], false);
        translate([0, pot_holder_width, 0]) {
            cube([pot_length_ext, pot_height, pot_holder_width], false);
        }
    }
    translate([0, pot_height/2, 0])
    {
        cube([pot_length_ext, pot_holder_width, pot_width], false);       
    }
}

module PotHolderPlatform() 
{
    pot_length_ext = pot_length + pot_ext;
    platform_height = wheel_radius_raise + wheel_radius;
    cube([pot_length_ext, pot_height/2 + pot_holder_width, platform_height]);
    translate([0, -(pot_height/2 + pot_holder_width), 0]) {
        cube([pot_length_ext, pot_height/2 + pot_holder_width, platform_height]);
    }
    translate([0, 0, platform_height]) 
    {
        PotHolderSide();
    }
}

PotHolderPlatform();
