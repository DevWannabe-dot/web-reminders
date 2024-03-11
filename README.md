# web-reminders
Repositório referente ao teste prático do processo seletivo para estagiar em Desenvolvimento de Software, em nome da dti digital.

# Premissas assumidas
Após analisar o modelo ilustrativo, disponibilizado nos termos do teste, uma premissa assumida foi a de conservar a exibição independentemente do nível de zoom no _viewport_. Ou seja, como o corte da imagem foi feito na vertical, apesar de o layout do navegador do modelo ser do tipo desktop, imaginei que os elementos devessem ocupar sempre o espaço da janela.

# Decisões de projeto
As decisões foram tomadas visando cumprir os requisitos exigidos pelo edital, sendo estes:
- Front-end feito sob o framework React.js, em Javascript;
- Back-end feito sob o framework .NET, em C#.

Foi decidido, também, o emprego da base de dados SQL Server, para manter os lembretes guardados pelo cliente (sem distinção entre usuários).

Nos "finalmentes" do projeto, encontrei uma grande dificuldade no desenvolvimento do _Front-End_ no meu ambiente escolar, porque as máquinas não possuiam node.js e nem havia como instalá-los. O que faltava para terminar o projeto era:
- O agrupamento dos lembretes por data, o qual eu ordenaria através da variável `newReminderDate`, tal qual definida na função `AddClick()`;
- Mostrar as datas dos lembretes, que eu faria inserindo após a linha 87 em `reminderfrontend/src/App.js` o código `{reminder.reminder_date}&nbsp;`;
- Estilizar o resto da aplicação web pela folha de estilos `reminderfrontend/src/App.css`, de tal forma que siga o modelo que se adapta ao display, conforme eu propus em [Premissas assumidas](#Premissas Assumidas). Era a parte mais fácil pra mim que recentemente completei meu curso de CSS pelo SoloLearn.
# Instruções para executar o sistema

### _Back-end_
1. ```
   cd reminderbackend/
   ```
2. ```
   dotnet run
   ```
3. Acessar [http://localhost:5066/swagger/index.html](http://localhost:5066/swagger/index.html)
### _Front-end_
1. ```
   cd reminderfrontend
   ```
2. ```
   npm install
   ```
3. ```
   npm start
   ```
