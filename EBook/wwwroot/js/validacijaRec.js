function ValidacijaRec(){
    if(komentarError('tekstKom') || selektErrorRec('selRating'))
        return false;
}


$("#potvrdiRec").click(function(){
    if(ValidacijaRec()==true){
        $("#FormaRec").css("display","none");
        $("#dodajRec").css("display","block");
        $("#dodajRecInfo").css("display","none");
    }
});


var el = document.getElementById('tekstKom');
el.onfocus=function(){
    this.onblur=()=>{komentarError('tekstKom');}
}
el.oninput=function(){
    komentarError('tekstKom');
}

function komentarError(inputId)
{
    var el = document.getElementById(inputId);
   
    if(el.value=="")
    {
        el.parentElement.querySelector(".error3").style.visibility="visible";
        el.parentElement.querySelector(".error3").style.display="block";
        return true;
    }
    else 
    {
        el.parentElement.querySelector(".error3").style.visibility="hidden";
        return false;
    }
}

var el1 = document.getElementById('selRating');
el1.onfocus=function(){
    this.onblur=()=>{selektErrorRec('selRating');}
}
el1.onchange=function(){
    selektErrorRec('selRating');
}


function selektErrorRec(selektId)
{
    var el = document.getElementById(selektId);
    if(el.selectedIndex == 0 && el.value == 0)
    {
        el.parentElement.querySelector(".error1").style.visibility="visible";
        el.parentElement.querySelector(".error1").style.display="block";
        return true;
    }
    else
    {
        el.parentElement.querySelector(".error1").style.visibility="hidden";
        return false;
    }
}