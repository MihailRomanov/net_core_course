using NUnit.Framework;
using System.ComponentModel;
using System.Threading;

namespace InternalDSL.Tests
{
    public class DialogModel
    {
        [DisplayName("Field 1111")]
        public string Field1 { get; set; }
        public int Field2 { get; set; }
    }

    [TestFixture]
    [RequiresThread(ApartmentState.STA)]
    public class DialogBuilderTests
    {
        [Test]
        public void TestDirectInit()
        {
            var dialog = DialogBuilder<DialogModel>.Create()
                .WithTitle("TestDirectInit")
                .WithField("Field 1", "Field1")
                .WithField("Field 2", "Field2")
                .Build(new DialogModel { Field1 = "TestDirectInit" });

            dialog.ShowDialog();
        }

        [Test]
        public void TestReflection()
        {
            var dialog = DialogBuilder<DialogModel>.Create()
                .WithTitleAndSize("TestReflection", 200, 200)
                .WithFieldsFromModel()
                .Build(new DialogModel { Field1 = "TestReflection" });

            dialog.ShowDialog();
        }

        [Test]
        public void TestExpression()
        {
            var model = new DialogModel { Field1 = "TestExpression", Field2 = 200 };

            var dialog = DialogBuilder<DialogModel>.Create()
                .WithTitleAndSize("TestExpression", 200, 200)
                .WithField(t => t.Field1)
                .WithField(t => t.Field2, "Name")
                .Build(model);

            dialog.ShowDialog();
        }
    }
}
