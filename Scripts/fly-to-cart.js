var flyingSpeed = 15;
var url_addProductToBasket = 'addProduct.php';
var url_removeProductFromBasket = 'removeProduct.php';

var shopping_cart_div = false;
var flyingDiv = false;
var currentProductDiv = false;

var shopping_cart_x = false;
var shopping_cart_y = false;

var slide_xFactor = false;
var slide_yFactor = false;

var diffX = false;
var diffY = false;

var currentXPos = false;
var currentYPos = false;
function addToBasket(productId) {
  if(!shopping_cart_div)shopping_cart_div = $('.cart');
  if(!flyingDiv){
    flyingDiv = document.createElement('DIV');
    //flyingDiv.className = 'flying-div';
    flyingDiv.style.position = 'absolute';
    document.body.appendChild(flyingDiv);
  }

  shopping_cart_x = $('.main-container').width()
                      - $('.main-container .container').width()
                      + shopping_cart_div.parents().eq(1).position().left;
  shopping_cart_y = shopping_cart_div.parents().eq(1).position().top;
  currentProductDiv = $('li.featured-prod#' + productId)
    .find('.card.front');
  
  currentXPos = currentProductDiv.parents().eq(4).position().left;
  currentYPos = currentProductDiv.parents().eq(4).position().top;
  
  diffX = shopping_cart_x - currentXPos;
  diffY = shopping_cart_y - currentYPos;
  
  var shoppingContentCopy = currentProductDiv.clone();
  $(shoppingContentCopy).attr('id', '');
  $(flyingDiv).html('')
    .css({'left':currentXPos + 'px',
          'top' :currentYPos + 'px'
        }).append(shoppingContentCopy);
  $(flyingDiv).css({'display':'block',
                    'width'  :$(currentProductDiv).css('width')
                  });
  flyToBasket(productId);
}

function flyToBasket(productId)
{
  var maxDiff = Math.max(Math.abs(diffX),Math.abs(diffY));
  var moveX = (diffX / maxDiff) * flyingSpeed;;
  var moveY = (diffY / maxDiff) * flyingSpeed;  
  
  currentXPos = currentXPos + moveX;
  currentYPos = currentYPos + moveY;
  
  flyingDiv.style.left = Math.round(currentXPos) + 'px';
  flyingDiv.style.top = Math.round(currentYPos) + 'px'; 
  
  
  if(moveX>0 && currentXPos > shopping_cart_x){
    flyingDiv.style.display='none';   
  }
  if(moveX<0 && currentXPos < shopping_cart_x){
    flyingDiv.style.display='none';   
  }
    
  if(flyingDiv.style.display=='block')
    setTimeout('flyToBasket("' + productId + '")',10);  
}