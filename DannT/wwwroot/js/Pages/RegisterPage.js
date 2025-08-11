import { formRegister, register,btnLogin} from "../Features/Register.js"
import { SwitchForm,formLogin} from "../Features/Login.js"

formRegister.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await register(formRegister);
    if (res)
        window.location.href = "/Feed";
    else {
        //Se cambiará para mostrar mensaje de error
        window.location.href = "/Auth/Forbidden";
    }
})
btnLogin.addEventListener("click", () => { SwitchForm(formLogin, formRegister) });