Sistema de agendamento e gestão de tarefas
Escopo: Produzir um sistema que permita o agendamento e manipulação de tarefas diárias, semanais, mensais. As tarefas serão cadastradas pelo coordenador de setor e desenvolvidas pelos funcionários do setor. Cada funcionário pode ver a agenda do setor. Ao selecionar a tarefa, o funcionário pode abrir e colocar a mudança de seu estado, de agendada para realizada. Nesse caso, o funcionário deve anexar uma evidência (foto) do cumprimento da tarefa. Deve existir hierarquia de usuários, sendo que os perfis de Gerente e Diretor poderão ver as tarefas de todos os setores e seus status.  

Requisitos Funcionais: 

Módulos:  
1 - Administração de usuários e perfis: 
Fazer o cadastro de usuários: nome, cpf, login, senha, setor.  
Fazer o cadastro de perfis: Nome, descrição. 
Fazer a associação/desassociação de usuários a perfis. 
Deve permitir o cadastro de políticas de controle de acesso. 
Deve permitir a associação/desassociação das políticas aos perfis. 
Sistema deve mostrar as funções de acordo com o perfil logado.  

2 - Agendamento de tarefas: 
Sistema deve permitir o agendamento de tarefas: Nome da tarefa, setor, data, horário de 
finalização. 
Sistema deve permitir que colaborador do setor veja as tarefas. 
Sistema deve permitir que colaborador edite a tarefa, troque o status e coloque evidência de 
realizada ou não. 
Sistema deve permitir o cancelamento da tarefa pelo perfil que possuir autorização. 
Sistema deve permitir a edição dos dados da tarefa pelo perfil que possuir autorização. 

3 - Consulta a agenda de tarefas: 
Sistema deve permitir a visualização de todas as tarefas agendadas. 
Sistema deve permitir a visualização das tarefas do dia. 
Sistema deve permitir a visualização das tarefas do dia por setor. 
Sistema deve permitir a visualização das tarefas por semana. 
Sistema deve permitir a visualização das tarefas por semana e por setor. 
Sistema deve permitir a visualização das tarefas por mês e por setor. 
Sistema deve permitir a visualização das tarefas por status (agendada, em andamento, realizada, cancelada). 
 
Requisitos não funcionais: 
Usar C# .NET. 
 
Criar camadas: 
 
Model - Onde ficarão as abstrações (Classes). 
 
Repository – Camada onde ficarão as classes de comunicação com banco de dados. 
                     Nomeclatura: ClasseRepository 
 
Data – Camada de configuração de banco de dados. 
 
Controller – Camada de construção das classes de API com os endpoints. 
                     Nomeclatura: ClasseController 
