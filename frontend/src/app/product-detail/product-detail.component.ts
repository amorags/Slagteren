import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent  implements OnInit {

  constructor() { }

  /**
(function() {
    document.querySelector('#AddProductForm_Form_Quantity_Holder input').type = 'number';
  })();

  var validateNumber = function (input) {
    if (parseInt(input.value) < parseInt(input.min)) {
      input.value = input.min;
    }
  };

  var incrementValueOfElement = function(elm){
    elm.value++;
    callEventChangeOnElement(elm);
  };

  var decrementValueOfElement = function(elm){
    elm.value--;
    callEventChangeOnElement(elm);
  };

  function callEventChangeOnElement(elm) {

    if (navigator.userAgent.indexOf('MSIE') !== -1 || navigator.appVersion.indexOf('Trident/') > 0) {
      var evt = document.createEvent('HTMLEvents');
      evt.initEvent("ie11PriceUpdate", false, true);
      document.dispatchEvent(evt);
    } else {
      var event = new Event('change');
      elm.dispatchEvent(event);
    }

  }
**/


  ngOnInit() {}

}
