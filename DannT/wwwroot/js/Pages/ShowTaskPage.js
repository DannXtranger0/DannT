import { form,loadTask,setUpTask,UpdateTask} from "../Features/ShowTask.js"
document.addEventListener("DOMContentLoaded", async () => {

    let task = await loadTask();
    await setUpTask(task);
    
})
form.addEventListener("submit", async (e) => {
    e.preventDefault();
    let res = await UpdateTask(form)
    if (res)
        window.location.href = "/Feed";
})

