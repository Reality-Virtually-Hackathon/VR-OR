flywheel_width = 25;
flywheel_height = 3;

flywheel_outer_width = 1;
flywheel_inner_width = 1;
flywheel_outer_lip = 1;

pot_width = 6.8/2;
strut_width = 4;
strut_hole = 0.75;

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
module OuterRim( makeTop)    {
    distanceToOuterRim = flywheel_width - flywheel_outer_width ;
    difference() {
        cylinder(flywheel_height, flywheel_width, flywheel_width, false);
        cylinder(flywheel_height, distanceToOuterRim, distanceToOuterRim, false);
    }
    outer_rim_wlip = flywheel_width + flywheel_outer_lip;
     difference() {
         cylinder(0.5, outer_rim_wlip, outer_rim_wlip, false);
         cylinder(flywheel_height, distanceToOuterRim, distanceToOuterRim, false);
    }
    
    if(makeTop) {
        //outer-rim-top-lid
        translate([distanceToOuterRim * 2+ 5, 0, 0])
        difference() {
             cylinder(0.5, outer_rim_wlip, outer_rim_wlip, false);
             cylinder(flywheel_height, distanceToOuterRim, distanceToOuterRim, false);
        }
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

module StrutWithHole() {
     strut_length = flywheel_width - flywheel_outer_width;
    difference() 
    {
        Strut();
        translate([0, strut_length - 5, 0]) {
            cylinder(flywheel_height, strut_hole, strut_hole, false);
        }
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

OuterRim(true);
InnerRim();
Struts();
 