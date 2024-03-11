import logo from './logo.svg';
import './App.css';
import { Component } from 'react';
class App extends Component{

  constructor(props){
    super(props);
    this.state={
      reminders:[]
    }
  }

  API_URL="http://localhost:5066/";

  componentDidMount(){
    this.refreshReminders();
  }

  async refreshReminders(){
    fetch(this.API_URL+"api/ReminderApp/Reminders").then(response=>response.json())
    .then(data=>{
      this.setState({reminders:data});
    })
  }

  async addClick(){
    var newReminder = document.getElementById("newReminder").value;
    const data=new FormData();
    data.append("newReminder", newReminder);

    fetch(this.API_URL+"api/ReminderApp/Reminder",{
      method:"POST",
      body:data
    }).then(res=>res.json())
    .then((result)=>{
      alert(result);
      this.refreshReminders();
    })
  }

  async deleteClick(id){
    fetch(this.API_URL+"api/ReminderApp/Reminder?id="+id,{
      method:"DELETE",
    }).then(res=>res.json())
    .then((result)=>{
      alert(result);
      this.refreshReminders();
    })
  }

  render() {
    const{reminders} = this.state;

    return (
      <div className="app">
        <h2>Novo lembrete</h2>
          <form classname="formStyle">
            <span className="labelSpanStyle">
            <label for="newReminder">Nome</label>
            </span>
            <input type="text" placeholder="Nome do lembrete" className="newReminder"/>&nbsp; <br/>
          </form>
          <form classname="formStyle">
            <span className="labelSpanStyle">
            <label for="newReminder">Data</label>
            </span>
            <input type="date" placeholder="Data do lembrete (no formato dd/mm/yyyy)" className="newReminder"/>&nbsp; <br/>
          </form>

          <button onClick={()=>this.addClick()} id="btnCriarStyle">Criar</button>
        {reminders.map(reminder=>
          <p>
            {reminder.description}&nbsp;
            <button onClick={()=>this.addClick(reminder.id)}>Delete Reminder</button>
          </p>)}
      </div>
    );
  }
}

export default App;
