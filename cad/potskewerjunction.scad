pot_width = 6.8/2;
pot_cap_width = 1;
pot_cap_height  = 5;

skewer_radius = 4.5/2;
skewer_pipe_length = (pot_width + pot_cap_width) *2;
skewer_rim = 1.0;

skewer_pipe_total = skewer_radius + skewer_rim;

$fn = 50;

module  PotCap() {
    distanceToInnerRim = pot_cap_width + pot_width;
    difference() {
        cylinder(pot_cap_height, distanceToInnerRim, distanceToInnerRim, false);    
        cylinder(pot_cap_height, pot_width, pot_width, false);
    }
    translate([0,0, pot_cap_height]) {
         cylinder(skewer_rim, distanceToInnerRim, distanceToInnerRim, false);
    }
}

module SkewerPipe() {
    
    difference() {
        cylinder(skewer_pipe_length, skewer_pipe_total, skewer_pipe_total, false);
        cylinder(skewer_pipe_length, skewer_radius, skewer_radius, false);
    }
}

module Complete() {
    PotCap();
    translate([0,skewer_pipe_length/2, pot_cap_height + skewer_pipe_total]) {
        rotate([90,0,0]) {
            SkewerPipe();
        }
    }
}


//Rotate and Center for cleaner printing
full_length = (pot_cap_height + skewer_pipe_total)/2;
rotate([90,0,0]) {
    translate([0,skewer_pipe_length/2, - full_length]) {
        Complete();
    }
}



