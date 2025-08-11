import { formLogin, login,btnRegister,SwitchForm} from "../Features/Login.js"
import { formRegister} from "../Features/Register.js"

formLogin.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await login(formLogin);
    if (res)
        window.location.href = "/Feed";
    else {
        //Se cambiará para mostrar mensaje de error
        window.location.href = "/Auth/Forbidden";
    }
})

btnRegister.addEventListener("click",()=>{
    SwitchForm(formLogin, formRegister);
});