window.onscroll = function() {scrollFunction()};
var btn=document.getElementById("button");
function scrollFunction() {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
btn.className="show";
  } else {
    btn.className="";
  }
}
btn.onclick=function(){
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

function openNav() {
    document.getElementById("mySidenav").style.left = "0"
  }
  
  function closeNav() {
    document.getElementById("mySidenav").style.left = "-500px";
}

function openNav2() {
  document.getElementById("resmySidenav").style.left = "0"
}

function closeNav2() {
  document.getElementById("resmySidenav").style.left = "-500px";
}


$('.slider').slick({
  dots: true,
  infinite: false,
  speed: 300,
  slidesToShow: 4,
  slidesToScroll: 4,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    }
  ]
});

let btnshow=document.querySelector(".dropdown_button");
btnshow.addEventListener("click",function(e) {
  var courses = document.getElementById("courses_id");

	if (courses.style.display == "block") {
		courses.style.display = "none";
    btnshow.classList.remove("btnshow");
	} else {
		courses.style.display = "block";
    btnshow.classList.add("btnshow");
	}
  e.stopPropagation()
}) 



window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  if (document.body.scrollTop > 250 || document.documentElement.scrollTop > 500) {
    document.getElementById("nav-2").style.top="0";
  } else {
    document.getElementById("nav-2").style.top="-120px";
  }
}







let btnnav=document.querySelector(".btnnav");
let divcontent=document.querySelector(".nav-content");
divcontent.style.height="0px"
btnnav.addEventListener("click",function(){
if (divcontent.style.height=="0px") {
  divcontent.style.height="180px";
}
else{
  divcontent.style.height="0px"
}
})