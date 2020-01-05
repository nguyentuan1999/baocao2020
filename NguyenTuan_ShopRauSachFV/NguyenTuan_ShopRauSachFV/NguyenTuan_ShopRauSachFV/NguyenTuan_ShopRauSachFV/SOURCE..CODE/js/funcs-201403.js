// JavaScript Document

function timeCOUNdown(id, h, m, s, txt) { 
	$('#'+id).countr({
		hours : h,
		minutes : m,
		seconds : s,
		callBack : function(me) {
			$(me).text(txt);
		}
	});
};

function isValidEmail(emailAddress) {
	var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);  
	return pattern.test(emailAddress);  
}

function showReplyComment(id, dealId, parentId, userImg, url) {		
	var html = '<div style=\"margin:10px 10px 0px 30px; padding:10px; border-top:2px solid #719a34;\"><form id=\"frmSendReplyComment\" name=\"frmSendReplyComment\" method=\"post\" action=\"'+url+'\" style=\"display:inline;\"><input type=\"hidden\" id=\"replyDealId\" name=\"replyDealId\" value=\"'+dealId+'\" /><input type="hidden" id=\"replyParentId\" name=\"replyParentId\" value=\"'+parentId+'\" /><div style=\"float:left; width:60px; height:40px; margin-left:10px;\"><img src=\"/images/users/'+userImg+'\" width=\"40\" height=\"40\" style=\"border:1px solid #d4d4d4; padding:5px;\" /></div><div style="float:left; width:200px; height:50px;"><div><textarea id=\"contentReplyComment\" name=\"contentReplyComment\" style=\"height:30px; width:485px; border:1px solid #d4d4d4;\"></textarea></div><div style=\"padding-top:2px;\"><input class=\"g-button g-button-submit\" name=\"btnReplyComment\" id=\"btnReplyComment\" value=\"&nbsp; Gửi bình luận nhanh &nbsp;\" type=\"submit\"></div></div><div class=\"clear\"></div></form></div>';	
	
	for(var i = 1; i < 500; i++) {		
		if(document.getElementById(id.substring(0,15)+i)) {
			if(id.substring(0,15)+i != id) {
				document.getElementById(id.substring(0,15)+i).style.display = 'none';
				document.getElementById(id.substring(0,15)+i).innerHTML = '';
			}
		}
	}	
	
	document.getElementById(id).style.display = 'block';
	document.getElementById(id).innerHTML = html;
	document.getElementById('contentReplyComment').focus();	
}

function hideReplyComment(id) {	
	document.getElementById(id).style.display = 'none';
	document.getElementById(id).innerHTML = '';
}

function showlayer(layer){
	var myLayer = document.getElementById(layer);
	if(myLayer.style.display=="none" || myLayer.style.display==""){
		myLayer.style.display="block";
	} else {
		myLayer.style.display="none";
	}
}

function hidelayer(layer) {
	var myLayer = document.getElementById(layer);
	myLayer.style.display="none";		
}




$( document ).ready(function() {							 	
							 
	$(".buy-btn").click(function() {	
		var selected = false;							   	
		for(i=1; i<10; i++) {
			if($('#qty-'+i)) {
				if($('#qty-'+i).val() > 0) {					
					selected = true;
				}
			}
		}	
		if(selected == false) {
			for(i=1; i<10; i++) {
				$('#qty-'+i).css('border', '1px dotted #f3831f');	
			}
			$('body,html').animate({scrollTop:0},600);
			alert('Bạn chưa chọn màu sắc, số lượng sản phẩm ?');
			return false;
		} else {
			document.forms.addToCard.submit();
		}
			
	});	// end validate	
	
							 
	// scroll to top 
	$(window).scroll(function() {
		if($(this).scrollTop() != 0) {
			$('#toTop').fadeIn();	
		} else {
			$('#toTop').fadeOut();
		}
	});
 
	$('#toTop').click(function() {
		$('body,html').animate({scrollTop:0},600);
	});
	// end scroll to top
	
	

     
        
	$("#menu-box li").hover(function(){
    	$('div', this).show();
        	$(this).addClass('hover');
        },function(){
        	$('div', this).hide();
    	$(this).removeClass('hover');
    });	
	
	
	// valiate send phone					   
	$("#btnSearch").click(function() {												   
		var txt = '';							
		if($('#tPhone').val() == ''){
			txt += 'Vui lòng nhập điện thoại để kiểm tra đơn hàng. \n';
		} else {					
			if($('#tPhone').val().length == 10 || $('#tPhone').val().length == 11 ){						
		} else {
			txt += 'Bạn nhập sai số điện thoại, vui lòng nhập lại. \n';
			}
		}		
		if(txt != '') {
			alert(txt);
			return false;
		} else {
			document.forms.frmSearch.submit();	
		}      
		return true;				   
													
	});	//end validate	
	
	
	
	
	// valiate send email					   
	$("#btnBuyWithoutRegister").click(function() {												   
		var txt = '';							
		if($('#nName').val() == ''){
			txt += 'Vui lòng nhập họ tên. \n';
		} 
		if($('#nPhone').val() == ''){
			txt += 'Vui lòng nhập số điện thoại di động. \n';
		} else {					
			if($('#nPhone').val().length == 10 || $('#nPhone').val().length == 11 ){
				var phone = $('#nPhone').val();
				var res = phone.substring(0,2); 
				if((res == '09') || (res == '01')) {
					if((res == '09') && ($('#nPhone').val().length == 11)) {
						txt += 'Bạn nhập sai số điện thoại, vui lòng nhập lại. \n';				
					}			
					if((res == '01') && ($('#nPhone').val().length == 10)) {
						txt += 'Bạn nhập sai số điện thoại, vui lòng nhập lại. \n';	
					}
				} else {
					txt += 'Vui lòng nhập số điện thoại di động. \n';
				}				
				
			} else {
				txt += 'Bạn nhập sai số điện thoại, vui lòng nhập lại. \n';
			}
		}		
						
		if($('#nDistrict').val() == ''){
			txt += 'Vui lòng chọn quận / huyện. \n';
		}
		if($('#nAddress').val() == ''){
			txt += 'Vui lòng nhập địa chỉ nhà. \n';
		}			
				
		if(txt != '') {
			alert(txt);
			return false;
		} else {
			document.forms.frmConfirm.submit();	
		}      
		return true;				   
													
	});	// end validate	
	
	//------------------------- view more home page -------------------------- //
	$("#view_more").click(function(event) {		
		/* stop form from submitting normally */
		event.preventDefault(); 
		$("#img-loading").show();
		$("#btn-more").hide();
		
		page = $("#num_page").val();
		url = '/home/';			
	
		/* Send the data using post and put the results in a div */
		$.post( url, { page: page, type:'ajax'},
		function( data ) {				
			if (data.success == 'yes') {				
				$( "#row" ).append( data.message );	
				$("#img-loading").hide();
				$("#btn-more").show();
				if(data.loading == 'no') {
					$( "#btn-more" ).empty();
				} 
				$( "#num_page" ).val(parseInt(page) + data.per_page);
				
				$("img.lazy").lazyload({
					effect : "fadeIn"
				});
				return true;					
            } else {	
				return false;					
			} 	  
			 
		  }, "json" );
	 });
	
	//------------------------- view more category page -------------------------- //
	$("#view_more_category").click(function(event) {		
		/* stop form from submitting normally */
		event.preventDefault(); 			
		$("#img-loading").show();
		$("#btn-more").hide();
		
		page = $("#num_page").val();
		url = $(location).attr('href');			
	
		/* Send the data using post and put the results in a div */
		$.post( url, { page: page, type:'ajax'},
		function( data ) {				
			if (data.success == 'yes') {				
				$( "#row" ).append( data.message );		
				$("#img-loading").hide();
				$("#btn-more").show();
				if(data.loading == 'no') {
					$( "#btn-more" ).empty();
				} 
				$( "#num_page" ).val(parseInt(page) + data.per_page);
				
				$("img.lazy").lazyload({
					effect : "fadeIn"
				});
				
				return true;					
            } else {	
				return false;					
			} 	  
			 
		  }, "json" );
	 });	
	
});

jQuery(document).ready(function($) {
  $('a[rel*=facebox]').facebox();
});







