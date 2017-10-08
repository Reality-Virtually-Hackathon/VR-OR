flywheel_width = 25;
flywheel_height = 3;

flywheel_outer_width = 1;
flywheel_inner_width = 1;

pot_width = 6.8/2;
strut_width = 2;

//resolution
$fn=50;


module  InnerRim() {
    distanceToInnerRim = flywheel_inner_width + pot_width;
    difference() {
        cylinder(flywheel_height, distanceToInnerRim, distanceToInnerRim, false);    
        cylinder(flywheel_height, pot_width, pot_width, false);
    }
}

//wheel with hole in the middle
module OuterRim()    {
    distanceToOuterRim = flywheel_width - flywheel_outer_width ;
    difference() {
        cylinder(flywheel_height, flywheel_width, flywheel_width, false);
        cylinder(flywheel_height, distanceToOuterRim, distanceToOuterRim, false);
    }
}

module Struts() {
    rotate([0, 0, 0]) { Strut();
        rotate([0, 0, 120]) { Strut();
            rotate([0, 0, 120]) { Strut();
    } } }
}

module Strut() {
     strut_length = flywheel_width - flywheel_outer_width;
     difference() {
        translate([-strut_width/2, 0, 0]) {
            cube([ strut_width, strut_length, flywheel_height ],false);
        }
        cylinder(flywheel_height, pot_width, pot_width, false);
    }
}


module HalfOuterRim() {
    difference() {
        OuterRim();
        translate([-flywheel_width, 0, 0], false) {
            cube([flywheel_width * 2,flywheel_width,flywheel_height], false);
        }
    }
}

OuterRim();
InnerRim();
Struts();
 