
   var p = 0, t = 81;
		//	$(window).scroll(function(e){
		//	p = $(this).scrollTop();
     	//	if(t < p){
		//	   $('.head').removeClass('headscroll');
    	//		$('.head').css({position:"relative"});
		//		$('.addhead').css({display:"none"});
				
     	//	}else{
		//		$('.head').addClass('headscroll');
		//		$('.head').css({position:"fixed",top:"0px"}).fadeIn(500);
		//		$('.addhead').css({display:"block"});
			    
     	//	}
		//	setTimeout(function(){
		//						t = p;
		//						}, 0);
		//});
 
	$(document).ready(function(){
	$(".user-ed .lot").hover(function(){
			$(this).addClass("hover");
			
		},function(){
			$(this).removeClass("hover");  
			
		}
	); 
	$(".nav-mo .nav-lis").hover(function(){
			$(this).addClass("hover");
		},function(){
			$(this).removeClass("hover");  
		}
	); 
	$(".toolbar .tool-lis").hover(function(){
			$(this).addClass("hover");
		},function(){
			$(this).removeClass("hover");  
		}
	); 
	

	
	 $(".seatext").each(function(){
     var thisVal=$(this).val();

     if(thisVal!=""){
       $(this).siblings("em").hide();
      }else{
       $(this).siblings("em").show();
      }
 
     $(this).focus(function(){
       $(this).siblings("em").hide();
      }).blur(function(){
        var val=$(this).val();
        if(val!=""){
         $(this).siblings("em").hide();
        }else{
         $(this).siblings("em").show();
        } 
      });
    })
	 
	 $(".seatext").each(function(){
     var thisVal=$(this).val();

     if(thisVal!=""){
       
	   $('.seabutton').addClass("focus");
      }else{
       
	   $('.seabutton').removeClass("focus");  
      }
      $(this).keyup(function(){
       var val=$(this).val();
       
	   $('.seabutton').addClass("focus");
      }).blur(function(){
        var val=$(this).val();
        if(val!=""){
         
		 $('.seabutton').addClass("focus");
        }else{
         
		 $('.seabutton').removeClass("focus");  
        }
       })
     }) 
	
    });