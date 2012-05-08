/* Inicialización en español para la extensión 'UI date picker' para jQuery. */
/* Traducido por Vester (xvester@gmail.com). */
(function ($) {
   var userAgent = navigator.userAgent.toLowerCase();
   jQuery.browser = {
       version: (userAgent.match(/.+(?:rv|it|ra|ie|me)[\/: ]([\d.]+)/) || [])[1],
       chrome: /chrome/.test(userAgent),
       safari: /webkit/.test(userAgent) && !/chrome/.test(userAgent),
       opera: /opera/.test(userAgent),
       msie: /msie/.test(userAgent) && !/opera/.test(userAgent),
       mozilla: /mozilla/.test(userAgent) && !/(compatible|webkit)/.test(userAgent)
   };

   $.Cspeech = {
       init: function () {
           if ($.browser.chrome) {
               $(this).each(function () {
                   //speech
                   var idtemp = $(this).attr("id") + "_C";
                   if ($("#" + idtemp).length == 0) {
                       if ($(this).attr("type") == "text") {
                           $(this).attr("x-webkit-speech", "x-webkit-speech");
                       } else if (!isNaN($(this).attr("rows")) || !isNaN($(this).attr("cols")) || $(this).attr("type") == "textarea") {
                           $(this).after('<input id="' + idtemp + '" type="text" x-webkit-speech="x-webkit-speech" style="width:16px; border:0px; background-color:Transparent;" onwebkitspeechchange="CspeechTranscribe(this,' + "'" + $(this).attr("id") + "'" + ')" />');
                       }
                   }
               });
           }
       }
   };

   $.fn.Cspeech = $.Cspeech.init;
})(jQuery);

function CspeechTranscribe(controlOri, controlDest) {
   //$("#" + controlDest).val($("#" + controlDest).val() + " " + $(controlOri).val());
   $("#" + controlDest).val($(controlOri).val());
   $(controlOri).val("");
   $("#" + controlDest).focus();
}