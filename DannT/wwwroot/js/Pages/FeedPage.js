import { form,Search,SetTasks} from "../Features/Feed.js"
document.addEventListener("DOMContentLoaded", async () => {
    let data = await Search(form);
    await SetTasks(data);
})
form.addEventListener("change", async (e) => {
    e.preventDefault();
    let res = 
        await Search(form);
    if (res) {

    }

})