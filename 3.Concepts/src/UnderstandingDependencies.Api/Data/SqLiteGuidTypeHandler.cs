﻿namespace UnderstandingDependencies.Api.Data;

public class SqLiteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value) => new ((string) value);
}
