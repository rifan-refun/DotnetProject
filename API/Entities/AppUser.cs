

// Entity di C# adalah class biasa yang merepresentasikan data bisnis dan struktur tabel database.
namespace API.Entities
{
    public class AppUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); // Id di generate otomatis pake Guid.
        // get untuk ngambil value, set untuk ngubah value.
        public required string UserName { get; set; }
        // UserName untuk menyimpan nama user. Required untuk menandakan bahwa property ini wajib diisi. kalo mau opsional, ganti required jadi nullable string (string?).
        public required string Emails { get; set; }
        // Emails untuk menyimpan email user. Required untuk menandakan bahwa property ini wajib diisi.
    }
}