function GetPlan(subPlanID){
  for(var indx = 0; indx < planJason.length; indx++){
          if(planJason[indx].SubPlanID == subPlanID){
            return planJason[indx];
          }
  }
}


function ResetLabel(obj,text,sty){
            if($(obj).val() == text){
            $(obj).val("");
            $(obj).removeClass("inputwithdefulttextcolor");
            }
            else if($(obj).val() == ""){
            $(obj).val(text);
            $(obj).addClass("inputwithdefulttextcolor");
            }

   }
     