using System;
namespace NIBO.Dominio.Util
{
    public class Mensagem
    {
        public static string Sucesso(){
            return "Operação realizada com sucesso!";
        }

        public static string Erro(){

            return "Ocorreu um erro durante a operaçao!";
        }

        public static string ErroDesafioEvento()
        {

            return "Informe o evento!";
        }

        public static string ErroDesafioEquipeIdentica()
        {

            return "Não é possivel cadastrar Desafio com as mesmas equipes!";
        }

        public static string ErroDesafioEquipeExistente()
        {

            return "A equipe selecionada já foi definida no desafio!";
        }

        public static string ErroDesafioEquipeSelecionar()
        {

            return "Selecione as equipes!";
        }

        public static string SemCadastroDesafios()
        {
            return "Não há cadastro de desafios para este evento!";
        }
    }
}
