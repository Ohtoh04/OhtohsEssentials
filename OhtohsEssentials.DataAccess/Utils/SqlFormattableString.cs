using System.Data;
using Microsoft.Data.SqlClient;
using static System.FormattableString;

namespace OhtohsEssentials.DataAccess.Utils;

/// <summary>
/// Creates a parameterized <see cref="SqlCommand"/> from an interpolated
/// <see cref="FormattableString"/>.
/// </summary>
/// <param name="conn">
/// The <see cref="SqlConnection"/> associated with the command.
/// </param>
/// <param name="formattableString">
/// An interpolated SQL string. The C# compiler separates the interpolated
/// string into a composite format string (for example,
/// <c>SELECT * FROM Users WHERE Name={0:NVarChar} AND Id={1:Int}</c>)
/// and an array of argument values.
/// </param>
/// <returns>
/// A <see cref="SqlCommand"/> whose SQL text contains generated parameter
/// names (<c>@p0</c>, <c>@p1</c>, ...) instead of the original format items,
/// with a corresponding <see cref="SqlParameter"/> created for each argument.
/// Format specifiers (such as <c>NVarChar</c> or <c>Int</c>) are interpreted
/// as <see cref="SqlDbType"/> values and assigned to the generated parameters.
/// </returns>
/// <remarks>
/// This method leverages <see cref="FormattableString"/> to transform an
/// interpolated SQL string into a parameterized query.
///
/// For example, the compiler represents:
/// <code>
/// $"SELECT Description
///   FROM Entries
///   WHERE Tag={tag:NVarChar}
///     AND UserId={userId:Int}"
/// </code>
///
/// as:
/// <code>
/// Format:
/// "SELECT Description
///  FROM Entries
///  WHERE Tag={0:NVarChar}
///    AND UserId={1:Int}"
///
/// Arguments:
/// [tag, userId]
/// </code>
///
/// This method then converts that representation into:
/// <code>
/// SELECT Description
/// FROM Entries
/// WHERE Tag=@p0
///   AND UserId=@p1
/// </code>
///
/// while creating the corresponding <see cref="SqlParameter"/> instances:
/// <code>
/// @p0 = tag    (SqlDbType.NVarChar)
/// @p1 = userId (SqlDbType.Int)
/// </code>
///
/// The resulting command is safe from SQL injection because user-supplied
/// values are passed as SQL parameters rather than being concatenated into
/// the SQL text.
/// </remarks>
public static class SqlFormattableString
{
    public static SqlCommand NewSqlCommand(
        this SqlConnection conn,
        FormattableString formattableString)
    {
        SqlParameter[] sqlParameters = formattableString.GetArguments()
            .Select((value, position) =>
                new SqlParameter(Invariant($"@p{position}"), value))
            .ToArray();

        object[] formatArguments = sqlParameters
            .Select(p => new FormatCapturingParameter(p))
            .ToArray();

        string sql = string.Format(
            formattableString.Format,
            formatArguments);

        var command = new SqlCommand(sql, conn);
        command.Parameters.AddRange(sqlParameters);

        return command;
    }

    private class FormatCapturingParameter : IFormattable
    {
        private readonly SqlParameter parameter;

        internal FormatCapturingParameter(SqlParameter parameter)
        {
            this.parameter = parameter;
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (!string.IsNullOrEmpty(format))
            {
                parameter.SqlDbType = (SqlDbType)Enum.Parse(
                    typeof(SqlDbType),
                    format,
                    true);
            }

            return parameter.ParameterName;
        }
    }
}
