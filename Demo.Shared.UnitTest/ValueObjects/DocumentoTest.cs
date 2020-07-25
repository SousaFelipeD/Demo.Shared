using Demo.Shared.Enums;
using Demo.Shared.ValueObjects;
using Xunit;

namespace Demo.Shared.UnitTest.ValueObjects
{
    public class DocumentoTest
    {
        [Theory]
        [InlineData("82318003017", ETipoDocumento.CPF, "823.180.030-17")]
        [InlineData("823.180.030-17", ETipoDocumento.CPF, "823.180.030-17")]
        [InlineData("90684978000102", ETipoDocumento.CNPJ, "90.684.978/0001-02")]
        [InlineData("24.450.575/0001-74", ETipoDocumento.CNPJ, "24.450.575/0001-74")]
        public void DeveRetornarTrueQuandoDocumentoEValido(string numero, ETipoDocumento tipoDocumento, string valorEsperado)
        {
            var documento = new Documento(numero, tipoDocumento);
            Assert.True(documento.Valid);
            Assert.Equal(valorEsperado, documento.ToFormat());
        }

        [Theory]
        [InlineData(null, ETipoDocumento.CPF)]
        [InlineData("", ETipoDocumento.CPF)]
        [InlineData("abcdgf", ETipoDocumento.CPF)]
        [InlineData("abcdgf", ETipoDocumento.CNPJ)]
        [InlineData("1234567895465482111", ETipoDocumento.CNPJ)]
        [InlineData("1234567895465482111", ETipoDocumento.CPF)]
        [InlineData("12345678911", ETipoDocumento.CPF)]
        [InlineData("12345678911", ETipoDocumento.CNPJ)]
        public void DeveRetornarFalseQuandoDocumentoEInvalido(string numero, ETipoDocumento tipoDocumento)
        {
            var documento = new Documento(numero, tipoDocumento);
            Assert.True(documento.Invalid);
            Assert.Equal(documento.Numero, documento.ToFormat());
        }
    }
}
