var colors = ['red', 'orange', 'yellow', 'cyan', 'blue', 'magenta', 'brown', 'white', 'black'];
var selectedColorInedx = 0;
var palette_cell_size = 30;
var x, y;
var drawing = false;

function setup() {
  createCanvas(800, 500);
  strokeWeight(10);
  background(250);
}

function draw() {
noStroke();
for(let i=0; i<colors.length; i++){
fill(colors[i]);
rect(0,i*palette_cell_size, palette_cell_size, palette_cell_size);
}
stroke(colors[selectedColorInedx]);
}

function mousePressed(){
  if(mouseX>= 0 && mouseX<palette_cell_size && mouseY>= 0 && mouseY<(colors.length * palette_cell_size)){
    selectedColorInedx = floor(mouseY/palette_cell_size);
    stroke(colors[selectedColorInedx]);
    drawing = false;
  }
  else{
    x = mouseX;
    y = mouseY;
    drawing = true;
  }
}

function mouseDragged(){
  if(drawing){
    line(x,y, mouseX, mouseY);
    x = mouseX;
    y = mouseY;
  }
}