using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AFS.DatabaseModel;

public class Translation
{
    public int Id { get; set; }
    public string FunLanguage { get; set; }
    public string InputText { get; set; }
    public string TranslatedText { get; set; }
}

public class TranslationTypeConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
    }
}
