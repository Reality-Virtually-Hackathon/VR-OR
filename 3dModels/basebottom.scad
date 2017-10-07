base_cap_diameter = 22;
base_cap_height = 1;
base_cap_radius = base_cap_diameter/2;

top_base_cap_height = 2.7;

base_height = 3.5;
base_diameter = 90;
base_radius = base_diameter/2;

strut_num = 4;
strut_width = 15;

$fn = 50;

module BaseCross() {
    strut_stride = 360 / strut_num;
    for (a =[0:strut_num]){
        rotate([0,0,strut_stride * a]) {
            translate([-strut_width/2, 0,0]) {
                cube([strut_width, base_radius, base_height]);
            }
        }
    }
}

module Base() {
    difference() {
        BaseCross();
        translate([0,0, base_height - base_cap_height]) {
            cylinder(base_height, base_cap_radius, base_cap_radius, false);
        }
    }
}

module BaseRadius() {
    base_rim = 5;
    difference() {
        cylinder(base_height, base_radius, base_radius, false);
        cylinder(base_height, base_radius -base_rim, base_radius - base_rim, false);
    }
}

module DrawBottom() {
    Base();
    BaseRadius();
}

module Base() {
    difference() {
        BaseCross();
        translate([0,0, base_height - top_base_cap_height]) {
            cylinder(top_base_cap_height, base_cap_radius, base_cap_radius, false);
        }
    }
}

module Top() {
    difference() {
        BaseCross();
        translate([0,0, base_height - base_cap_height]) {
            cylinder(top_base_cap_height, base_cap_radius, base_cap_radius, false);
        }
    }
}

module DrawTop() {
    notch_width = 4.5;
    notch_from_edge = 25;
    difference() {
        
        union() {
            Base();
            BaseRadius();
        }
        
        translate([- notch_width/2, base_radius-notch_from_edge, 0]) {
            cube([notch_width, notch_from_edge, base_height]);
        }
    }
}

DrawTop();

