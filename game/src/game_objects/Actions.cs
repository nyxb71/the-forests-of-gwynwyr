
namespace game {
    interface IGo {
        void Go(Zone z);
    }

    interface ILook {
        string Look(Direction dir);
    }
}
