using System;
namespace NIBO.Dominio.Util
{
    public class MessageUtil
    {
        public static string Sucess(){
            return "Operação realizada com sucesso!";
        }

        public static string Error(){

            return "Ocorreu um erro durante a operaçao!";
        }

        public static string ErrorDesafioEvento()
        {

            return "Informe o evento!";
        }

        public static string ErrorDesafioEquipeIdentica()
        {

            return "Não é possivel cadastrar Desafio com as mesmas equipes!";
        }

        public static string ErrorDesafioEquipeExistente()
        {

            return "A equipe selecionada já foi definida no desafio!";
        }

        public static string ErrorDesafioEquipeSelecionar()
        {

            return "Selecione as equipes!";
        }

        public static string SemCadastroDesafios()
        {
            return "Não há cadastro de desafios para este evento!";
        }
    }
}
