using ServerAPI.Models.HocSinh;

namespace ServerAPI.Services.Interface
{
    public interface IMonHocService
    {
        public Task<bool> AddMonHoc(MonHoc monHoc);
        public Task<bool> RemoveMonHoc(int maMH);
    }
}
