using AutoFixture;
using onofrej.github.io;
using SampleWorker.Application.UseCases.CreateOrder.DataAccess;

namespace SampleWorker.IntegrationTests.Tests;

[Collection("Test collection")]
public class OrderConsumerTests : BaseIntegratedTest
{
    private const int NumberOfRetries = 10;
    private const int SleepDurantionProviderInSeconds = 2;
    private readonly IFixture _fixture = new Fixture();
    private readonly MainFixture _mainFixture;

    public OrderConsumerTests(MainFixture mainFixture) => _mainFixture = mainFixture;

    [Fact(DisplayName = "Message is consumed and broker notes already exist in the database then broker notes is updated in the database")]
    public async Task MessageConsumedAndBrokerNotesAlreadyExistInDatabaseThenBrokerNotesIsUpdated()
    {
        //  //Arrange
        //  var message = _fixture.Create<OrderEvent>();

        // var entity = _fixture.Build<OrderEntity>() .With(propertyPicker =>
        // propertyPicker.InvestmentId, input!.InvestmentId) .With(propertyPicker =>
        // propertyPicker.BrokerNoteId, input.BrokerNoteId) .With(propertyPicker =>
        // propertyPicker.BrokerNoteNumber, input.BrokerNoteNumber) .With(propertyPicker =>
        // propertyPicker.GrossValueAmount, input.GrossValue.ToCurrency()) .With(propertyPicker =>
        // propertyPicker.GrossValueCurrency, input.Currency) .With(propertyPicker =>
        // propertyPicker.BrokerageFeeAmount, input.BrokerageFee.ToCurrency()) .With(propertyPicker
        // => propertyPicker.BrokerageFeeCurrency, input.Currency) .With(propertyPicker =>
        // propertyPicker.ClearingSettlementFeeAmount, input.ClearingSettlementFee.ToCurrency())
        // .With(propertyPicker => propertyPicker.ClearingSettlementFeeCurrency, input.Currency)
        // .With(propertyPicker => propertyPicker.ClearingRegistrationFeeAmount,
        // input.ClearingRegistrationFee.ToCurrency()) .With(propertyPicker =>
        // propertyPicker.ClearingRegistrationFeeCurrency, input.Currency) .With(propertyPicker =>
        // propertyPicker.StockExchangeAssetTradeNoticeFeeAmount,
        // input.StockExchangeAssetTradeNoticeFee.ToCurrency()) .With(propertyPicker =>
        // propertyPicker.StockExchangeAssetTradeNoticeFeeCurrency, input.Currency)
        // .With(propertyPicker => propertyPicker.StockExchangeFeeAmount,
        // input.StockExchangeFee.ToCurrency()) .With(propertyPicker =>
        // propertyPicker.StockExchangeFeeCurrency, input.Currency) .With(propertyPicker =>
        // propertyPicker.ClearingCustodyFeeAmount, input.ClearingCustodyFee.ToCurrency())
        // .With(propertyPicker => propertyPicker.ClearingCustodyFeeCurrency, input.Currency)
        // .With(propertyPicker => propertyPicker.TaxesAmount, input.Taxes.ToCurrency())
        // .With(propertyPicker => propertyPicker.TaxesCurrency, input.Currency)
        // .With(propertyPicker => propertyPicker.IncomeTaxAmount, input.IncomeTax.ToCurrency())
        // .With(propertyPicker => propertyPicker.IncomeTaxCurrency, input.Currency)
        // .With(propertyPicker => propertyPicker.NetValueAmount, input.NetValue.ToCurrency())
        // .With(propertyPicker => propertyPicker.NetValueCurrency, input.Currency) .Create();

        // await _mainFixture.DynamoDbFixture.InsertAsync(entity);

        // var newGrossValue = _fixture.Create<double>(); var newBrokerageFee =
        // _fixture.Create<double>(); var newClearingSettlementFee = _fixture.Create<double>(); var
        // newClearingRegistrationFee = _fixture.Create<double>(); var
        // newStockExchangeAssetTradeNoticeFee = _fixture.Create<double>(); var newStockExchangeFee
        // = _fixture.Create<double>(); var newClearingCustodyFee = _fixture.Create<double>(); var
        // newTaxes = _fixture.Create<double>(); var newIncomeTax = _fixture.Create<double>(); var
        // newNetValue = _fixture.Create<double>();

        // message.data.valor_operacao = newGrossValue; message.data.valor_corretagem_negocio =
        // newBrokerageFee; message.data.valor_emolumento_cblc = newClearingSettlementFee;
        // message.data.valor_taxa_registro = newClearingRegistrationFee;
        // message.data.valor_taxa_aviso_negociacao_acao = newStockExchangeAssetTradeNoticeFee;
        // message.data.valor_emolumento_bolsa = newStockExchangeFee;
        // message.data.valor_taxa_custodia_clearing = newClearingCustodyFee;
        // message.data.valor_imposto_renda_operacao_day_trade = newTaxes;
        // message.data.valor_imposto_renda_retido_fonte = newIncomeTax;
        // message.data.valor_liquido_nota_corretagem = newNetValue;

        //  //Act
        //  _mainFixture.KafkaFixture.ProduceBrokeNotesMessage(message);

        //  //Assert
        //  var brokerNotesEntity = await base.ExecutePolicyAsync(entity => entity != null && entity?.GrossValueAmount != newGrossValue.ToString(),
        //NumberOfRetries, SleepDurantionProviderInSeconds,
        //() => _mainFixture.DynamoDbFixture.ReadAsync<OrderEntity>(OrderEntity.HashKeyName, brokerNoteId));

        // brokerNotesEntity.Should().NotBeNull();
        // brokerNotesEntity!.GrossValueAmount.Should().Be(newGrossValue.ToCurrency());
        // brokerNotesEntity!.BrokerageFeeAmount.Should().Be(newBrokerageFee.ToCurrency());
        // brokerNotesEntity!.ClearingSettlementFeeAmount.Should().Be(newClearingSettlementFee.ToCurrency());
        // brokerNotesEntity!.ClearingRegistrationFeeAmount.Should().Be(newClearingRegistrationFee.ToCurrency());
        // brokerNotesEntity!.StockExchangeAssetTradeNoticeFeeAmount.Should().Be(newStockExchangeAssetTradeNoticeFee.ToCurrency());
        // brokerNotesEntity!.StockExchangeFeeAmount.Should().Be(newStockExchangeFee.ToCurrency());
        // brokerNotesEntity!.ClearingCustodyFeeAmount.Should().Be(newClearingCustodyFee.ToCurrency());
        // brokerNotesEntity!.TaxesAmount.Should().Be(newTaxes.ToCurrency());
        // brokerNotesEntity!.IncomeTaxAmount.Should().Be(newIncomeTax.ToCurrency()); brokerNotesEntity!.NetValueAmount.Should().Be(newNetValue.ToCurrency());
    }

    [Fact(DisplayName = "Message is consumed and broker notes does not exist in the database then broker notes is inserted in the database")]
    public async Task MessageConsumedAndBrokerNotesNotExistInDatabaseThenBrokerNotesIsInserted()
    {
        //Arrange
        var message = _fixture.Create<OrderEvent>();

        //Act
        _mainFixture.KafkaFixture.ProduceBrokeNotesMessage(message);

        //Assert
        var OrderEntity = await base.ExecutePolicyAsync(entity => entity == null,
          NumberOfRetries, SleepDurantionProviderInSeconds, () => _mainFixture.DynamoDbFixture.ReadAsync<OrderEntity>(Application.UseCases.CreateOrder.DataAccess.OrderEntity.HashKeyName, brokerNoteId));

        OrderEntity.Should().NotBeNull();
        OrderEntity?.BrokerNoteId.Should().Be(brokerNoteId);
    }
}