$(document).ready(function(){

    $(".product-mini-img img").click(function(){
        console.log("ok")
        var image = $(this).attr("src");
        $(".product-wrapper-img img").attr("src",image);
    })
});




$(".product-wrapper-img")
.on("mouseover", function() {
  $(this)
    .children("img")
    .css({ transform: "scale(" + $(this).attr("data-scale") + ")" });
})
.on("mouseout", function() {
  $(this)
    .children("img")
    .css({ transform: "scale(1)" });
})
.on("mousemove", function(e) {
  $(this)
    .children("img")
    .css({
      "transform-origin":
        ((e.pageX - $(this).offset().left) / $(this).width()) * 100 +
        "% " +
        ((e.pageY - $(this).offset().top) / $(this).height()) * 100 +
        "%"
    });
});




$(document).ready(function(){
  
  $('#stars li').on('mouseover', function(){
    var onStar = parseInt($(this).data('value'), 10); 
   
    $(this).parent().children('li.star').each(function(e){
      if (e < onStar) {
        $(this).addClass('hover');
      }
      else {
        $(this).removeClass('hover');
      }
    });
    
  }).on('mouseout', function(){
    $(this).parent().children('li.star').each(function(e){
      $(this).removeClass('hover');
    });
  });
  
  
  /* 2. Action to perform on click */
  $('#stars li').on('click', function(){
    var onStar = parseInt($(this).data('value'), 10); // The star currently selected
    var stars = $(this).parent().children('li.star');
    
    for (i = 0; i < stars.length; i++) {
      $(stars[i]).removeClass('selected');
    }
    
    for (i = 0; i < onStar; i++) {
      $(stars[i]).addClass('selected');
    }
    
    // JUST RESPONSE (Not needed)
    var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
    var msg = "";
    if (ratingValue > 1) {
        msg = "Thanks! You rated this " + ratingValue + " stars.";
    }
    else {
        msg = "We will improve ourselves. You rated this " + ratingValue + " stars.";
    }
    responseMessage(msg);
    
  });
  
  
});
$(function () {
  var tab = $(".tab_one"),
    content = $(".tab_body");

  tab.filter(":first").addClass("active");

  content.filter(":not(:first)").hide();

  tab.click(function () {
    var indeks = $(this).index();
    tab.removeClass("active").eq(indeks).addClass("active");
    console.log(indeks);
    content.hide().eq(indeks).fadeIn(500);
    // console.log(content);
    return false;
  });
});


let btnall=document.querySelectorAll(".btnclick");
let divs =document.querySelectorAll(".tabcontent");
for (let btn of btnall) {
  btn.addEventListener("click",function(){
    let active =document.querySelector(".active");
    console.log(active);
    active.classList.remove("active")
    this.classList.add("active")
    let index=this.getAttribute("id");
    for (let div of divs) {
     if(index===div.getAttribute("id")){
      div.style="display:block";
     }
     else{
      div.style="display:none";
     }
    }
  })
}
