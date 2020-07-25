using Demo.Shared.Enums;
using Flunt.Validations;
using System;

namespace Demo.Shared.ValueObjects
{
    public class Documento : ValueObject
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="tipoDocumento"></param>
        public Documento(string numero, ETipoDocumento tipoDocumento)
        {
            Numero = numero;
            TipoDocumento = tipoDocumento;

            AddNotifications(new Contract()
                    .IsTrue(Validade(), nameof(Numero), "O número do documento não é válido")
                    );

        }

        /// <summary>
        /// Número
        /// </summary>
        public string Numero { get; private set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public ETipoDocumento TipoDocumento { get; private set; }

        /// <summary>
        /// Número do documento formatado
        /// </summary>
        public string ToFormat() 
        {
            if (Valid)
            {
                string format = Numero;

                switch (TipoDocumento)
                {
                    case ETipoDocumento.CPF:
                        format = Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
                        break;
                    case ETipoDocumento.CNPJ:
                        format = Convert.ToUInt64(Numero).ToString(@"00\.000\.000\/0000\-00");
                        break;
                }

                return format;
            }

            return Numero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retorna o número do documento</returns>
        public override string ToString()
        {
            return Numero;
        }

        private bool Validade()
        {
            if (string.IsNullOrEmpty(Numero))
            {
                return false;
            }

            Numero = Numero.Replace("-", "").Replace(".", "").Replace("/", "").Trim();

            if (TipoDocumento == ETipoDocumento.CNPJ)
            {
                return IsCnpj(Numero);
            }

            return IsCpf(Numero);

        }

        private bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
            {
                return false;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;
            }

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            
            if (cnpj.Length != 14)
            {
                return false;
            }

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
